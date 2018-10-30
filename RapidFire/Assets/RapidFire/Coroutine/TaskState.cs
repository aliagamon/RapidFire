﻿namespace RapidFire.Coroutine
{
    /// <summary>
    /// Running state of a task.
    /// </summary>
    public enum TaskState
    {
        /// <summary>
        /// Task has been created, but has not begun.
        /// </summary>
        Init,
        /// <summary>
        /// Task is running.
        /// </summary>
        Running,
        /// <summary>
        /// Task has finished properly.
        /// </summary>
        Done,
        /// <summary>
        /// Task has been cancelled.
        /// </summary>
        Cancelled,
        /// <summary>
        /// Task terminated by errors.
        /// </summary>
        Error
    }
}