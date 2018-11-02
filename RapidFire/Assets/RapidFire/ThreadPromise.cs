using System;
using System.Collections.Generic;
using System.Threading;
using Boo.Lang.Runtime.DynamicDispatching;

namespace RapidFire
{
    public class ThreadPromise : IPromise
    {
        public class Factory
        {
            private Action _task;
            private Action _finally;
            private Queue<Action> _thens = new Queue<Action>();

            public static Factory Start()
            {
                return new Factory();
            }

            public Factory Task(Action task)
            {
                _task = task;
                return this;
            }

            public Factory Finally(Action @finally)
            {
                _finally = @finally;
                return this;
            }

            public Factory Then(Action then)
            {
                _thens.Enqueue(then);
                return this;
            } 

            public ThreadPromise Promise()
            {
                return new ThreadPromise(_task, _finally, _thens);
            }
        }

        public ThreadPromise(Action task, Action @finally, Queue<Action> thens)
        {
            _task = task;
            _finally = @finally;
            _thens = thens;
            Queue();
        }

        public Thread Thread { get; private set; }

        public bool Launched
        {
            get { return Thread != null; }
        }
        
        public bool IsDone { get; private set; }

        public Exception Exception { get; private set; }

        private readonly Action _task;
        private readonly Action _finally;
        private readonly Queue<Action> _thens;

        public void Await()
        {
            while (true)
            {
                if (IsDone)
                    return;
                if(Thread.CurrentThread == RF.Current.MainThread)
                    RF.Current.Update();
            }
        }

        private void Queue()
        {
            ThreadPool.QueueUserWorkItem(Launcher, _task);
        }

        private void Launcher(object action)
        {
            Thread = Thread.CurrentThread;
            try
            {
                ((Action)action)();
                foreach (var then in _thens)
                {
                    then();
                }
            }
            catch (Exception e)
            {
                Exception = e;
            }
            finally
            {
//                Interlocked.Decrement(ref _numThreads);
                _finally();
                IsDone = true;
            }
        }
    }
}
