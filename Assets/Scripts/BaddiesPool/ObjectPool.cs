using System;
using System.Collections.Generic;

public class ObjectPool<T>
{
    private readonly Func<T> _preloadFunc;
    private readonly Action<T> _getAction;
    private readonly Action<T> _returnAction;

    private Queue<T> _objectPool = new();
    private List<T> _active = new();

    public ObjectPool(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount)
    {
        _preloadFunc = preloadFunc;
        _getAction = getAction;
        _returnAction = returnAction;
        
        if (preloadFunc == null)
        {
            return;
        }

        for (int i = 0; i < preloadCount; i++)
        {
            Return(preloadFunc());
        }
    }

    public T Get()
    {
        T item = _objectPool.Count > 0 ? _objectPool.Dequeue() : _preloadFunc();
        _getAction(item);
        _active.Add(item);

        return item;
    }

    public void Return(T item)
    {
        _returnAction(item);
        _objectPool.Enqueue(item);
        _active.Remove(item);
    }

    public void ReturnAll()
    {
        for (int i = 0; i < _active.Count; i++)
        {
            _returnAction(_active[i]);
            _objectPool.Enqueue(_active[i]);
        }

        for (int i = 0; i < _active.Count; i++)
        {
            _active.Remove(_active[i]);
        }

        _active.Clear();
    } 
}
