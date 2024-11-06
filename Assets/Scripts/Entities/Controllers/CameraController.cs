using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool canRotate;

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineComposer composer;
    private CinemachineTransposer transposer;
    private Vector2 mouseDelta;

    [SerializeField] private Vector3 defaultFollowOffset;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private Vector3 trackedObjectOffset = new Vector3(0, 0.6f, 0);
    [SerializeField] private float zoomMin = 3f;
    [SerializeField] private float zoomMax = 8f;
    [SerializeField] private float buildModeZoom = 10f;
    private float saveZoom = 0f;

    private void Start()
    {
        InitCamera();
    }

    private void InitCamera()
    {
        GameObject virtualCameraGameObject = new GameObject("VirtualCamera");
        virtualCamera = virtualCameraGameObject.AddComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow = transform;
        virtualCamera.LookAt = transform;
        transposer = virtualCamera.AddCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = defaultFollowOffset;
        // WorldSpace = 카메라가 월드 공간에서 타겟의 위치만 따라가고 회전은 영향을 받지 않습니다.
        transposer.m_BindingMode = CinemachineTransposer.BindingMode.WorldSpace;
        composer = virtualCamera.AddCinemachineComponent<CinemachineComposer>();
        composer.m_TrackedObjectOffset = trackedObjectOffset;
        // 떨림 방지
        transposer.m_XDamping = 0f;
        transposer.m_YDamping = 0f;
        transposer.m_ZDamping = 0f;
        composer.m_DeadZoneWidth = 0f;
        composer.m_DeadZoneHeight = 0f;
        composer.m_HorizontalDamping = 0f;
        composer.m_VerticalDamping = 0f;

    }

    public void MouseRightPressed(bool isPressed)
    {
        canRotate = isPressed;
        Cursor.lockState = isPressed ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void Zoom(float delta)
    {
        if (GameManager.Instance.IsBuildMode) return;

        float zoom = transposer.m_FollowOffset.y - (delta * zoomSpeed * Time.deltaTime);
        transposer.m_FollowOffset.y = Mathf.Clamp(zoom, zoomMin, zoomMax);
        saveZoom = transposer.m_FollowOffset.y;
    }

    public void RotateCamera(Vector2 mouseDelta)
    {
        if (!canRotate) return;

        float horizontalRotation = mouseDelta.x * rotationSpeed * Time.deltaTime;
        transposer.m_FollowOffset = Quaternion.AngleAxis(horizontalRotation, Vector3.up) * transposer.m_FollowOffset;

        // 카메라가 캐릭터를 계속 바라보도록 설정
        virtualCamera.m_LookAt = virtualCamera.Follow;
    }
    public void BuildMode(bool isBuildMode)
    {
        if (isBuildMode) 
        {
            transposer.m_FollowOffset.y = buildModeZoom;
            composer.m_TrackedObjectOffset = Vector3.zero;
        }
        else
        {
            transposer.m_FollowOffset.y = saveZoom;
            composer.m_TrackedObjectOffset = trackedObjectOffset;
        }
    }
}
