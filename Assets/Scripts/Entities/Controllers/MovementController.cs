using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Vector3 moveDirection;
    private Rigidbody rigidbody;

    [Header("임시 이동속도")]
    public float speed = 5;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = moveDirection * speed;
    }

    public void Move(Vector3 move)
    {
        moveDirection = move.normalized;
    }
}
