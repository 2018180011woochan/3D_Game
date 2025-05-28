using System.Collections;
using UnityEngine;

public class MANAGER : MonoBehaviour
{
    public static MANAGER instance = null;
    public static PoolManager POOL;
    public static DBManager DB;
    public static SessionManager SESSION;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        POOL = GetComponentInChildren<PoolManager>();
        DB = GetComponentInChildren<DBManager>();
        SESSION = GetComponentInChildren<SessionManager>();
    }

    public void Run(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
    
}
