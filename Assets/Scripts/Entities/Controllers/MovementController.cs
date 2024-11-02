using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MovementController : MonoBehaviour
{
    private Vector3 moveDirection;
    [SerializeField] float axisY;
    private Rigidbody rb;
    private CharacterAnimController anim;

    [Header("임시 이동속도")]
    public float moveSpeed = 3f;
    public float acceleration = 0.1f;

    private float currentSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<CharacterAnimController>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (moveDirection == Vector3.zero)
        {
            rb.velocity = Vector3.zero;
            anim.IdleAnim();
        }
        else
        {
            ApplyMove(moveDirection);
        }
    }
    private void ApplyMove(Vector3 dir)
    {
        CharacterMoveStepType type = CharacterMoveStepType.Idle;
        float forwardDot = Vector3.Dot(transform.forward, moveDirection);
        float rightDot = Vector3.Dot(transform.right, moveDirection);
        // 화면에서 볼때 캐릭터가 아래로 보면 z축이 -1, 위로 보면 z축이 1, 왼쪽으로 보면 x축이 -1, 오른쪽으로 보면 x축이 1 이다.
        // 이동 방향이 위쪽(즉 z 축이 1)이고 캐릭터가 바라보는 방향아 아래쪽일 때, 캐릭터는 뒷걸음질 치는 모션을 취해야하고, 캐릭터가 바라보는 방향으로 이동 할때는 전진하는 모션을 취해야하고, 바라보는 방향에서 좌, 우로 움직일때는 사이드 스텝으로 모션을 취해야한다.
        float maxMoveSpeed = moveSpeed;
        if (forwardDot > 0.5)
        {
            maxMoveSpeed = moveSpeed;
            type = CharacterMoveStepType.Forward;
        }
        else if (forwardDot < -0.5)
        {
            maxMoveSpeed = moveSpeed * 0.5f;
            type = CharacterMoveStepType.Backward;
        }
        else if (rightDot > 0.5)
        {
            maxMoveSpeed = moveSpeed * 0.75f;
            type = CharacterMoveStepType.Right;
        }
        else if (rightDot < -0.5)
        {
            maxMoveSpeed = moveSpeed * 0.75f;
            type = CharacterMoveStepType.Left;
        }

        if (rb.velocity.magnitude < maxMoveSpeed)
        {
            //rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
            Vector3 acc = dir * acceleration;
            rb.velocity += acc;
        }
        anim.MoveAnim(type);
    }

    public void Move(Vector3 move)
    {
        moveDirection = move;
    }
}
