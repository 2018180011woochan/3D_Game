using UnityEngine;

public class DBManager : MonoBehaviour
{
    public PartDB Monster;
    public PartDB Bullet;

    private void Start()
    {
        Monster = GetDB("Monster");
        Bullet = GetDB("Bullet");
    }

    private PartDB GetDB(string path)
    {
        return Resources.Load<PartDB>("DB/" + path);
    }
}
