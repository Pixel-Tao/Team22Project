using Defines;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Vector3 moveDirection;
    [SerializeField] float axisY;
    private Rigidbody rb;
    private CharacterAnimController anim;
    private Condition condition;

    [Header("임시 이동속도")]
    public float defaultMoveSpeed = 3f;
    public float acceleration = 0.1f;

    public float MoveSpeed => condition == null ? defaultMoveSpeed : condition.CurrentStat.moveSpeed;

    [Header("이동 방향에 따른 애니메이션 변경 범위")]
    [SerializeField][Range(0, 1)] private float forwardDotRange = 0.5f;
    [SerializeField][Range(0, 1)] private float rightDotRange = 0.5f;

    [Header("이동 방향에 따른 속도 비율")]
    [SerializeField][Range(0, 1)] private float backwardSpeedRatio = 0.5f;
    [SerializeField][Range(0, 1)] private float sideSpeedRatio = 0.75f;

    [Header("타일 확인")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckInterval = 0.1f;
    private TileObject onTile;
    private float groundCheckTime;
    private Camera mainCamera;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<CharacterAnimController>();
        condition = GetComponent<Condition>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        GroundCheck();
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

    private void ApplyMove(Vector3 inputDirection)
    {
        CharacterMoveStepType type = CharacterMoveStepType.Forward;
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();
        Vector3 cameraRight = mainCamera.transform.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();
        Vector3 direction = cameraForward * inputDirection.z + cameraRight * inputDirection.x;
        direction.Normalize();
        float forwardDot = Vector3.Dot(transform.forward, direction);
        float rightDot = Vector3.Dot(transform.right, direction);
        // 화면에서 볼때 캐릭터가 아래로 보면 z축이 -1, 위로 보면 z축이 1, 왼쪽으로 보면 x축이 -1, 오른쪽으로 보면 x축이 1 이다.
        // 이동 방향이 위쪽(즉 z 축이 1)이고 캐릭터가 바라보는 방향아 아래쪽일 때, 캐릭터는 뒷걸음질 치는 모션을 취해야하고, 캐릭터가 바라보는 방향으로 이동 할때는 전진하는 모션을 취해야하고, 바라보는 방향에서 좌, 우로 움직일때는 사이드 스텝으로 모션을 취해야한다.
        // 양 방향간의 내적으로 캐릭터의 움직임을 정해 애니메이션을 취하도록 한다.
        float maxMoveSpeed = MoveSpeed;
        if (forwardDot < -forwardDotRange)
        {
            maxMoveSpeed = maxMoveSpeed * backwardSpeedRatio;
            type = CharacterMoveStepType.Backward;
        }
        else if (rightDot > rightDotRange)
        {
            maxMoveSpeed = maxMoveSpeed * sideSpeedRatio;
            type = CharacterMoveStepType.Right;
        }
        else if (rightDot < -rightDotRange)
        {
            maxMoveSpeed = maxMoveSpeed * sideSpeedRatio;
            type = CharacterMoveStepType.Left;
        }

        if (rb.velocity.magnitude < maxMoveSpeed)
        {
            Vector3 velocity = direction * acceleration;
            rb.velocity += velocity;
        }
        anim.MoveAnim(type);
    }

    public void Move(Vector3 move)
    {
        moveDirection = move;
    }

    public void GroundCheck()
    {
        if (Time.time - groundCheckTime > groundCheckInterval)
        {
            groundCheckTime = Time.time;
            Ray ray = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit, 1f, groundLayer))
            {
                if (hit.collider.transform.parent != null &&
                    hit.collider.transform.parent.TryGetComponent(out TileObject tile))
                {
                    if (onTile != tile)
                    {
                        onTile?.PlayerOnTile(false);
                        onTile = tile;
                        onTile?.PlayerOnTile(true);
                    }
                }
                else
                {
                    onTile.PlayerOnTile(false);
                    onTile = null;
                }
            }
        }
    }

}
