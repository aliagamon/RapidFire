using System;

namespace RapidFire
{
    public interface IPromise
    {
        void Await();
        Exception Exception { get; }
        bool Launched { get; }
        bool IsDone { get; }
    }
}