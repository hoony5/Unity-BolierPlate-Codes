using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class TraceEx
{
    private static StackTrace _stackTrace = new StackTrace(true);
    private static Stack<TraceInfo> _traceInfos = new Stack<TraceInfo>();
    private static HashSet<string> _bannedFileNames = new HashSet<string>
    {
        "TraceEx.cs",
        "Naughty",
        "Editor",
    };

    private static bool ValidateTraceInfo(StackFrame frame, string fileName)
    {
        if (!frame.HasSource()) return false;
        if (string.IsNullOrEmpty(fileName)) return false;
        
        return !_bannedFileNames.Any(part => fileName.Contains(part));
    }

    public static void Logger(this Component context, string msg)
    {
#if UNITY_EDITOR
        StackFrame[] frames = _stackTrace.GetFrames();

        int startIndex = 1;
        ColorLog.Lime($"Message : {msg}");
        for (var index = startIndex; index < frames?.Length; index++)
        {
            StackFrame frame = frames[index];
            string fileName = frame.GetFileName();
            if (!ValidateTraceInfo(frame, fileName)) continue;
            fileName = Path.GetFileName(fileName);
            ColorLog.Lime(BuildStringToFormat(fileName,
                frame.GetMethod().Name,
                frame.GetFileLineNumber(),
                msg), context.gameObject);

            _traceInfos.Push(new TraceInfo
            {
                Component = context,
                FileName = fileName,
                MethodName = frame.GetMethod().Name,
                LineNumber = frame.GetFileLineNumber()
            });
        }
#endif
    }

    private static string BuildStringToFormat(string fileName,string methodName, int lineNumber, string msg)
    {
        StringBuilder sb = new StringBuilder();
          sb.AppendLine("==========================================")
            .AppendFormat("ComponentType : {0} | Location : {1} | Line : {2},",
                Path.GetFileNameWithoutExtension(fileName),
                methodName,
                lineNumber)
            .AppendLine()
            .AppendLine("==========================================")
            .AppendFormat("File : <a = href=\"{0}\">{1}</a>", fileName, fileName);
        return sb.ToString();
    }
    public static bool TraceNext(string msg)
    {
#if UNITY_EDITOR
        if (_traceInfos.Count <= 0) return false;
        TraceInfo traceInfo = _traceInfos.Pop();
        ColorLog.Lime(BuildStringToFormat(traceInfo.FileName, traceInfo.MethodName, traceInfo.LineNumber, msg), traceInfo.Component.gameObject);
        string path = $"Assets\\{traceInfo.FileName}";
        AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<MonoScript>(path), traceInfo.LineNumber);
#endif
        return true;
    }

    public static void Clear() => _traceInfos.Clear();
}

public class TraceInfo
{
    public Component Component { get; set; }
    public string FileName { get; set; }
    public string MethodName { get; set; }
    public int LineNumber { get; set; }
}
