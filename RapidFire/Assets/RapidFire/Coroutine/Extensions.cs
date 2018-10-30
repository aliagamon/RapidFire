using System.Collections;
using UnityEngine;

namespace RapidFire.Coroutine
{
    public static class Extentions
    {
        /// <summary>
        /// Start a co-routine on a background thread.
        /// </summary>
        /// <param name="task">Gets a task object with more control on the background thread.</param>
        /// <returns></returns>
        public static UnityEngine.Coroutine StartCoroutineAsync(
            this MonoBehaviour behaviour, IEnumerator routine, 
            out Task task)
        {
            return Co.StartCoroutineAsync(behaviour, routine, out task);
        }

        /// <summary>
        /// Start a co-routine on a background thread.
        /// </summary>
        public static UnityEngine.Coroutine StartCoroutineAsync(
            this MonoBehaviour behaviour, IEnumerator routine)
        {
            Task t;
            return StartCoroutineAsync(behaviour, routine, out t);
        }
    }
}