using UnityEngine;
using System.Collections.Generic;
using System;


public class ObjectPool : IPool
{
    public Transform parentTransform { get; set; }
    public Queue<GameObject> pool { get; set; } = new Queue<GameObject>();

    public GameObject Get(Action<GameObject> action = null)
    {
        GameObject obj = pool.Dequeue();
        obj.SetActive(true);
        if (action != null)
        {
            action?.Invoke(obj);
        }

        return obj;
    }

    public void Return(GameObject obj, Action<GameObject> action = null)
    {
        pool.Enqueue(obj);
        obj.transform.parent = parentTransform;
        obj.SetActive(false);

        if (action != null)
        {
            action?.Invoke(obj);
        }
    }
}

public class PoolManager : MonoBehaviour
{
    public Dictionary<String, IPool> m_Pool_Dictionary = new Dictionary<string, IPool> ();

    Transform baseObj = null;

    private void Start()
    {
        baseObj = this.transform;
    }
    public IPool PoolingObj(string path)
    {
        if (m_Pool_Dictionary.ContainsKey(path) == false) 
        {
            AddPool(path);
        }
        if (m_Pool_Dictionary[path].pool.Count <= -0) AddQueue(path);
        return m_Pool_Dictionary[path];
    }

    private GameObject AddPool(string path)
    {
        GameObject obj = new GameObject(path + "##POOL");
        obj.transform.SetParent(baseObj);
        ObjectPool tPool = new ObjectPool();

        m_Pool_Dictionary.Add(path, tPool);
        tPool.parentTransform = obj.transform;
        return obj;
    }

    private void AddQueue(string path)
    {
        var obj = Instantiate(Resources.Load<GameObject>("POOL/" + path));

        m_Pool_Dictionary[path].Return(obj);
    }
}
