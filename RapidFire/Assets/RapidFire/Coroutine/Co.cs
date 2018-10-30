using System.Collections;
using UnityEngine;

namespace RapidFire.Coroutine
{
	public class Co
	{
		public static readonly object Merge;
		public static readonly object Branch;

		static Co()
		{
			Merge = new object();
			Branch = new object();
		}

		internal static UnityEngine.Coroutine StartCoroutineAsync(MonoBehaviour behaviour, IEnumerator routine,
			out Task task)
		{
			task = new Task(routine);
			return behaviour.StartCoroutine(task);
		}
	}
}
