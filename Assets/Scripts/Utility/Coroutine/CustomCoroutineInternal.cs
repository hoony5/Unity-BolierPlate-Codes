using System.Collections;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct CustomCoroutineInternal
{
    public int index;
    public IEnumerator routine;
    public CustomCoroutineToken token;

    public int Index => index;
    public IEnumerator Routine => routine;
    public CustomCoroutineToken Token => token;

    public CustomCoroutineInternal(int index, IEnumerator routine, CustomCoroutineToken token)
    {
        this.index = index;
        this.routine = routine;
        this.token = token;
    }
    public CustomCoroutineInternal Init(int index, IEnumerator routine, CustomCoroutineToken token)
    {
        this.index = index;
        this.routine = routine;
        this.token = token;
        return this;
    }

    public CustomCoroutineToken OnStart() => Token.OnStart();
    public CustomCoroutineToken OnAsync() => Token.OnAsync();
    public CustomCoroutineToken OnSync() => Token.OnSync();
    public CustomCoroutineToken OnPause() => Token.OnPause();
    public CustomCoroutineToken OnStop() => Token.OnStop();
    public bool KeepWaiting(bool isNull) => Token.KeepWaiting(isNull);

    public CustomCoroutineInternal Reset()
    {
        index = -1;
        routine = null;
        token = default;
        return this;
    }
}