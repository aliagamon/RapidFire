using RapidFire;
using UnityEngine;

namespace TestCases
{
    public class SyncingTestCase : MonoBehaviour
    {
        private void Awake()
        {
//            Debug.Log("sync awake first log");
//            RF.RunAsync(() =>
//            {
//                Debug.Log("sync thread first log");
//                RF.QueueOnMainThread(() => Debug.Log("sync main thread log")).Await();
//                Debug.Log("sync thread second log");
//            }).Await();
//            Debug.Log("sync awake second log");
            RF.RunAsync(() =>
            {
                while (true)
                {
                    RF.QueueOnMainThread(() =>
                        transform.Rotate(30 * Time.deltaTime, 30 * Time.deltaTime, 30 * Time.deltaTime)).Await();
                }
            });
        }
    }
}