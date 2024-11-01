using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offsetPosition;
    public float offsetAngle = 45;

    private Transform cameraRoot;
    private Camera cam;

    private Quaternion initialRotation;

    private void Start()
    {
        SetCameraTarget();
        initialRotation = cameraRoot.transform.rotation;
    }

    private void LateUpdate()
    {
        cam.transform.localPosition = offsetPosition;
        cam.transform.localEulerAngles = new Vector3(offsetAngle, 0, 0);
        cameraRoot.transform.rotation = initialRotation;
    }

    public void SetCameraTarget()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "CameraRoot")
            {
                cameraRoot = child;
                break;
            }
        }

        if (cameraRoot == null)
        {
            GameObject go = new GameObject("CameraRoot");
            go.transform.SetParent(transform);
            cameraRoot = go.transform;
        }
        cam = Camera.main;
        cam.transform.SetParent(cameraRoot);
    }
}
