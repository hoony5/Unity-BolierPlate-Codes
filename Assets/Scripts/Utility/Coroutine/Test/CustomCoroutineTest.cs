using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class CustomCoroutineTest : MonoBehaviour
{
   public CustomCoroutine coroutine;
   private WaitForTime _waitForTime = new WaitForTime(1);
   [SetUp]
   public void Setup()
   {
      coroutine ??= new GameObject().AddComponent<CustomCoroutine>();
      coroutine.Init();
   }

   [Test, Button]
   public void Test_NormalRoutine()
   {
      CustomCoroutineToken token = coroutine.AddRoutine(TestRoutine());
      coroutine.StartRoutines();
      bool exist = coroutine.HasRoutine(token);
      Assert.AreEqual(true, exist);
   }
   [Test, Button]
   public void Test_NormalRoutineWithTag()
   {
      CustomCoroutineToken token = coroutine.AddRoutineWithTag("Test",TestRoutine());
      coroutine.StartRoutines();
      bool exist = coroutine.AnyExist("Test");
      
      Assert.AreEqual(true, exist);
   }
   [Test, Button]
   public void Test_NormalRoutineNested()
   {
      CustomCoroutineToken token = coroutine.AddRoutine(TestRoutine0());
      coroutine.StartRoutines();
      bool exist = coroutine.HasRoutine(token);
      Assert.AreEqual(true, exist);
      
   }
   [Test, Button]
   public void Test_NormalRoutineNestedIndexCheck()
   {
      CustomCoroutineToken token = coroutine.AddRoutine(TestRoutine00());
      coroutine.StartRoutines();
      bool exist = coroutine.HasRoutine(token);
      Assert.AreEqual(true, exist);
      
   }

   [UnityTest]
   public IEnumerator TestRoutine()
   {
      {
         Debug.Log("before Wait");
         yield return _waitForTime;
         Debug.Log("after Wait");   
      }
   }
   
   [UnityTest]
   public IEnumerator TestRoutine0()
   {
      Debug.Log("before Wait0");
      yield return TestRoutine1();
      Debug.Log("after Wait0");
   }
   [UnityTest]
   public IEnumerator TestRoutine00()
   {
      Debug.Log("before Wait00");
      yield return TestRoutine1();
      Debug.Log("after Wait00");
   }
   [UnityTest]
   public IEnumerator TestRoutine1()
   {
      Debug.Log("before Wait1");
      yield return TestRoutine2();
      Debug.Log("after Wait1");
   }
   [UnityTest]
   public IEnumerator TestRoutine2()
   {
      Debug.Log("before Wait2");
      yield return _waitForTime;
      Debug.Log("after Wait2");
   }
   
}
