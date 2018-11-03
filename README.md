# RapidFire
A thread pool system for unity3d<br>
You can use it to do some heavy tasks on another thread and then merging the task with main thread so you can apply it to componnets.<br>
Unity3d by nature isn't thread safe at all and it don't come with a working dispatcher so we need a work around!<br>
RapidFire provides you API and tools for multi-thread programming inside of Unity3d, It uses ThreadPool for async tasks, and for getting back to main thread you have to queue it for the next update call.<br>
It also comes with ThreadNinja as a lib implemented within it so you can use async croutines and threads togather.
# Road Map
I'm planning on making a way so you can update `UnityEngine.Object`s in an easier way.<br>
RapidFire need a way for batching queue tasks comming from a single function call so they apply all togather.<br>
_It's an work in progress project and huge changes in stucture is possible so in current state of project i don't gurantee backward compatibility for future updates, but i will try my best to keep it compatible._