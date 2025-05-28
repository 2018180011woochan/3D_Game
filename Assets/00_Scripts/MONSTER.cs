using UnityEngine;

public class MONSTER : MonoBehaviour
{
    public int HP;
    public int MaxHP;

    public Transform target;

    public string monsterId;
    private IFactory<MONSTER> factory;

    protected bool isSpanwed = false;
    public bool isDead = false;

    public virtual void Initalize(Transform player)
    {
        isSpanwed = false;
        HP = 10;
        MaxHP = HP;

        isDead = false;

        monsterId = Random.Range(0, 2) == 1 ? "Skeleton_01" : "Skeleton_02";

        factory = new GenericPartFactory<MONSTER>(MANAGER.DB.Monster);

        target = player;
        factory.Build(this, monsterId);
    }

    public void GetDamage(int dmg)
    {
        HP -= dmg;

        var damageFont = MANAGER.POOL.PoolingObj("DamageTMP").Get((value) =>
        {
            value.GetComponent<DamageTMP>().Initalize(
                BaseCanvas.instance.transform,
                transform.position,
                dmg.ToString());
        });

        if (HP <= 0)
        {
            isDead = true;

            var deadEffect = MANAGER.POOL.PoolingObj("DeadEffect").Get((value) =>
            {
                value.transform.position = transform.position;
            });

            MANAGER.instance.Run(UtilCoroutine.Delay(0.5f,
                () => MANAGER.POOL.m_Pool_Dictionary["DeadEffect"].Return(deadEffect)));

            MANAGER.POOL.m_Pool_Dictionary["Monster"].Return(this.gameObject);

            DropEXP(transform.position, Random.Range(1.0f, 5.0f));
        }
    }

    private void DropEXP(Vector3 deathPosition, float exp = 1.0f)
    {
        float[] units = { 3.0f, 1.0f, 0.25f };

        foreach(float unit in units)
        {
            while (exp >= unit)
            {
                exp -= unit;

                OrbMake(deathPosition, unit);
            }
        }

        if (exp > 0.01f)
        {
            OrbMake(deathPosition, exp);
        }
    }

    private void OrbMake(Vector3 deathPosition, float exp)
    {
        Vector3 spawnPos = deathPosition + UtilsWorld.GetRandomCircleOffset(1.5f);

        var orb = MANAGER.POOL.PoolingObj("Orb").Get((value) =>
        {
            value.transform.position = transform.position;
            value.GetComponent<Orb>().Initalize(exp, spawnPos);
        });
    }
}
