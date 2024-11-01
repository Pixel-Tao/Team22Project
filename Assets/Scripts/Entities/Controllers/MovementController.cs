using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Vector3 moveDirection;
    [SerializeField] float axisY;
    private Rigidbody rb;

    [Header("임시 이동속도")]
    public float speed = 5;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
    }

    public void Move(Vector3 move)
    {
        moveDirection = move * speed;
    }
}
