using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f;
    public float lifetime = 5.0f;
    public GameObject ExplosionParticle;
    public GameObject DamageObject;
    private Vector3 direction;

    public void Initalize(Vector3 dir)
    {
        direction = dir;
        StartCoroutine(DestroyCoroutine(lifetime));
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    IEnumerator DestroyCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
    }   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            Instantiate(ExplosionParticle, transform.position, Quaternion.identity);

            other.gameObject.GetComponent<MONSTER>().GetDamage(MANAGER.SESSION.Damage);

            StopAllCoroutines();
            MANAGER.POOL.m_Pool_Dictionary["Bullet"].Return(this.gameObject);
        }
    }
}
