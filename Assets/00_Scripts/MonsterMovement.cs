using UnityEngine;
using System.Collections;
public class MonsterMovement : MONSTER
{
    public float speed = 3.0f;
    private Rigidbody rb;
    private Animator animator;
    bool isSpanwed = false;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    public void SetTarget(Transform player)
    {
        target = player;
    }

    public override void Initalize(Transform player)
    {
        base.Initalize(player);
        Rotate(direction(), false);

        StartCoroutine(SpawnStartCoroutine(transform.localScale));
    }

    IEnumerator SpawnStartCoroutine(Vector3 scaleEnd)
    {
        Vector3 ScaleStart = Vector3.zero;
        Vector3 ScaleEnd = scaleEnd;

        float duration = 0.5f;
        float timer = 0.0f;

        while(timer < duration)
        {
            float t = timer / duration;
            transform.localScale = Vector3.Lerp(ScaleStart, ScaleEnd, t);
            timer += Time.deltaTime;

            yield return null;
        }
        isSpanwed = true;
        animator.SetTrigger("MOVE");
    }

    public void FixedUpdate()
    {
        if (!isSpanwed) return;
        
        MoveAndRotate();
    }

    void MoveAndRotate()
    {
        Rotate(direction());

        rb.MovePosition(rb.position + direction() * speed * Time.fixedDeltaTime);
    }

    Vector3 direction()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        //transform.position += direction * speed * Time.deltaTime;
        direction.y = 0f;

        return direction;
    }

    void Rotate(Vector3 direction, bool Lerp = true)
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetRotaion = Quaternion.LookRotation(direction);
            if (Lerp)
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotaion, 10f * Time.deltaTime);
            else transform.rotation = targetRotaion;

        }
    }
}
