using System.Collections.Generic;

public class CustomCoroutineInternalPool
{
    private readonly Stack<CustomCoroutineInternal> _pool = new Stack<CustomCoroutineInternal>();

    public CustomCoroutineInternal Get()
    {
        if (_pool.TryPop(out CustomCoroutineInternal item))
            return item;

        return new CustomCoroutineInternal();
    }

    public void Return(CustomCoroutineInternal item)
    {
        item = item.Reset();
        _pool.Push(item);
    }
}