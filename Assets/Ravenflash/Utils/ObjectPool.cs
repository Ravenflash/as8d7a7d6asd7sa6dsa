using UnityEngine;
using System.Collections.Generic;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private Stack<T> _pool = new Stack<T>();

    /// <summary>
    /// Constructor to set the prefab
    /// </summary>
    /// <param name="prefab">Pooled object prefab</param>
    public ObjectPool(T prefab)
    {
        _prefab = prefab;
    }

    /// <summary>
    /// Creates a new instance of type T and adds it to the pool
    /// </summary>
    /// <returns>Pooled object.</returns>
    public T Get(Transform parent =null)
    {
        if (_pool.Count > 0)
        {
            T obj = _pool.Pop();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            return Object.Instantiate(_prefab, parent);
        }
    }

    /// <summary>
    /// Adds an object to the pool
    /// </summary>
    /// <param name="obj">Object to be returned to the pool.</param>
    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Push(obj);
    }
}