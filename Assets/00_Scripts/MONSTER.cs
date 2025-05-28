using UnityEngine;

public class MONSTER : MonoBehaviour
{
    public Transform target;

    public string monsterId;
    private IFactory<MONSTER> factory;

    public virtual void Initalize(Transform player)
    {
        monsterId = Random.Range(0, 2) == 1 ? "Skeleton_01" : "Skeleton_02";

        factory = new GenericPartFactory<MONSTER>(MANAGER.DB.Monster);

        target = player;
        factory.Build(this, monsterId);
    }
}
