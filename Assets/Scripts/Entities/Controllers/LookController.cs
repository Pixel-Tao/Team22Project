using UnityEngine;

public class LookController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer; // 지면 레이어 마스크
    private Vector3 mouseDelta;
    private bool stopRotation;
    private Camera mainCamera;
    private float lookSpeed = 5;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (stopRotation) return;

        // 화면 좌표를 레이캐스트로 변환
        Ray ray = mainCamera.ScreenPointToRay(mouseDelta);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundLayer))
        {
            Vector3 targetPosition = hitInfo.point;
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0f; // 수직 방향은 무시

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
    }

    public void Look(Vector2 delta)
    {
        mouseDelta = delta;
    }

    public void MouseRightPressed(bool isPressed)
    {
        stopRotation = isPressed;
    }
}
