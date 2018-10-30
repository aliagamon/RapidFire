using System.Collections;
using UnityEngine;
using RapidFire;

namespace TestCases
{
	public class ExecuteAction : MonoBehaviour 
	{
		// Use this for initialization
		private void Start ()
		{
			var promise = RF.RunAsync(() =>
			{
				Debug.unityLogger.Log("From rapid fire!");
//					transform.Rotate(10, 10, 10, Space.Self);
			});
			StartCoroutine(Check(promise));
		}

		private IEnumerator Check(ThreadPromise promise)
		{
			while (true)
			{
				Debug.unityLogger.Log(promise.Launched);
				yield return null;
			}
		}
	}
}
