using System;
using System.Diagnostics;
using NaughtyAttributes;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TestC : MonoBehaviour
{
    public TestA testA;
    public StackTrace stackTrace;
                                    
    [Button()]
    private void Test()
    {                   
        LogTrace("hihihi");
    }

    public int lineNumber;
    void LogTrace(string message)
    {
       this.Logger(message);
    }
    [TraceLogic]
    public void SomeMethod(int param1, int param2)
    {
        TraceLogicInfo log = new TraceLogicInfo()
        {
            ParameterValues = new []{ param1.ToString(), param2.ToString() },
            ReturnType = typeof(void).Name,
            ClassName = nameof(TestC),
            MethodName = nameof(SomeMethod)
        };
        TraceLogicCache.Instance.Add(log);
        Debug.Log($"Executing {log.ClassName}.{log.MethodName} with parameters of types {string.Join(", ", log.ParameterValues)} and return type {log.ReturnType}.");
        
        // Method logic here.
    }

    private void Start()
    {
        SomeMethod(1, 2);
    }
}
