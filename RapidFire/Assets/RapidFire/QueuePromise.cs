using System;
using System.Threading;

namespace RapidFire
{
    public class QueuePromise : IPromise
    {
        private readonly Action _task;
        public float Time { get; private set; }

        public QueuePromise(Action task, float time)
        {
            _task = task;
            Time = time;
        }

        public void Await()
        {
            while(true)
            {
                if(IsDone)
                    return;
                if(Thread.CurrentThread == RF.Current.MainThread)
                    RF.Current.Update();
            }
        }

        public Exception Exception { get; private set; }
        public bool Launched { get; private set; }
        public bool IsDone { get; private set; }

        public void Launch()
        {
            Launched = true;
            try
            {
                _task();

            }
            catch (Exception e)
            {
                Exception = e;
            }
            finally
            {
                IsDone = true;
            }
        }
    }
}