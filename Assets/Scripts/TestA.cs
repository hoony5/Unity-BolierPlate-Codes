using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TestA : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private string msg;
    public void Test(string Message)
    {
        msg = Message; 
        StackTrace stackTrace = new StackTrace(true); // 'true' means get file and line number info
        StackFrame[] frames = stackTrace.GetFrames();
        for (var index = 0; index < frames.Length; index++)
        {
            StackFrame frame = frames[index];
            ColorLog.Orange($@"
{index} : {frame}
HasMethod : {frame.HasMethod()}
HasSource : {frame.HasSource()}
Method : {frame.GetMethod().Name},
File : {frame.GetFileName()},
Line : {frame.GetFileLineNumber()},
Column : {frame.GetFileColumnNumber()}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
