using System.Diagnostics;
using NaughtyAttributes;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TestC : MonoBehaviour
{
    public StackTrace stackTrace;

    [Button()]

    private void Test()
    {
        LogTrace("hihihi");
    }

    public int lineNumber;
    void LogTrace(string message)
    {
        StackTrace stackTrace = new StackTrace(true); // 'true' means get file and line number info
        StackFrame frame = stackTrace.GetFrame(lineNumber); // Get calling method frame. Change the index based on your needs

        string fileName = frame.GetFileName();
        int _lineNumber = frame.GetFileLineNumber();

        DebugEx.Lime($"Trace: {fileName}:{_lineNumber}");
        Debug.Log("This log message will include a full stack trace.");
    }
}
