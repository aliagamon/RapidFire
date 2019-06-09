using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace RapidFire
{
    public class RF : MonoBehaviour
    {
        public Thread MainThread { get; private set; }

        public static int MaxThreads { get; set; }

        private static int _numThreads;

        private static RF _current;
        private int _count;
        public static RF Current
        {
            get
            {
                Initialize();
                return _current;
            }
        }

        private void Awake()
        {
            _current = this;
            _initialized = true;
        }

        private static bool _initialized;

        private static void Initialize()
        {
            if (_initialized) return;
            if (!Application.isPlaying)
                return;
            _initialized = true;
            var g = new GameObject("RapidFire");
            _current = g.AddComponent<RF>();
            _current.MainThread = Thread.CurrentThread;
        }

        private readonly List<QueuePromise> _actions = new List<QueuePromise>();
        public struct DelayedQueueItem
        {
            public float Time;
            public Action Action;
        }

        private readonly List<QueuePromise> _delayed = new List<QueuePromise>();

        private readonly List<QueuePromise> _currentDelayed = new List<QueuePromise>();

        public static QueuePromise QueueOnMainThread(Action action, float time = 0f)
        {
            if (!Application.isPlaying) return QueuePromise.FailPromise;
            var promise = new QueuePromise(action, time);
            if (Math.Abs(time) > Utilities.Math.ToLerance)
            {
                lock (Current._delayed)
                {
                    Current._delayed.Add(promise);
                }
            }
            else
            {
                lock (Current._actions)
                {
                    Current._actions.Add(promise);
                }
            }

            return promise;
        }

        public static ThreadPromise RunAsync(Action a)
        {
            Initialize();
            while (_numThreads >= MaxThreads)
            {
                Thread.Sleep(1);
            }
            //			Interlocked.Increment(ref _numThreads);
            //			System.Threading.ThreadPool.QueueUserWorkItem(RunAction, a);
            //			return null;

            Interlocked.Increment(ref _numThreads);
            return ThreadPromise.Factory.Start().Task(a).Finally(() => Interlocked.Decrement(ref _numThreads))
                .Promise();
        }

        /*
                private static void RunAction(object action)
                {
                    try
                    {
                        ((Action)action)();
                    }
                    catch
                    {
                        // ignored
                    }
                    finally
                    {
                        Interlocked.Decrement(ref _numThreads);
                    }

                }
        */


        private void OnDisable()
        {
            if (_current == this)
            {

                _current = null;
            }
        }


        private readonly List<QueuePromise> _currentActions = new List<QueuePromise>();

        static RF()
        {
            MaxThreads = 8;
        }

        // Update is called once per frame
        public void Update()
        {
            lock (_actions)
            {
                _currentActions.Clear();
                _currentActions.AddRange(_actions);
                _actions.Clear();
            }
            foreach (var a in _currentActions)
            {
                a.Launch();
            }
            lock (_delayed)
            {
                _currentDelayed.Clear();
                _currentDelayed.AddRange(_delayed.Where(d => d.Time <= Time.time));
                foreach (var item in _currentDelayed)
                    _delayed.Remove(item);
            }
            foreach (var delayed in _currentDelayed)
            {
                delayed.Launch();
            }



        }
    }
}
