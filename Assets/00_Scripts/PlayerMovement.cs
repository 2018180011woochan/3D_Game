using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public Vector3 cameraDir = Vector3.zero;

    
    private Camera myCamera;
    private Vector3 moveDir;

    CharacterController controller;
    private Animator animator;

    private void Start()
    {
        myCamera = Camera.main;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
        Animate();
        CameraMove();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(h, 0, v).normalized;

        controller.SimpleMove(moveDir * moveSpeed);
    }

    void Rotate()
    {
        if (Player.instance.target != null)
        {
            Vector3 dirToMonster = Player.instance.Direction();
            dirToMonster.y = 0;

            RotateToQuaternion(dirToMonster);
        }
        else if (moveDir.sqrMagnitude > 0.01f)
        {
            RotateToQuaternion(moveDir);
        }
    }

    void RotateToQuaternion(Vector3 direction)
    {
        Quaternion targetRot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 10f * Time.deltaTime);
    }
    void Animate()
    {
        animator.SetFloat("SPEED", moveDir.magnitude);
    }

    void CameraMove()
    {
        myCamera.transform.position = Vector3.Lerp(
            myCamera.transform.position,
            transform.position + cameraDir,
            2.0f + Time.deltaTime);
    }
}
