using UnityEngine;

public class MANAGER : MonoBehaviour
{
    public static MANAGER instance = null;
    public static PoolManager POOL;
    public static DBManager DB;

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
    }

    
}
