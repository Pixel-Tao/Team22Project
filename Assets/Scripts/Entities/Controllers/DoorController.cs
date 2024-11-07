using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Transform leftDoor;
    [SerializeField] private float leftDoorOpenAngle = 90f;

    [SerializeField] private Transform rightDoor;
    [SerializeField] private float rightDoorOpenAngle = -90f;

    public void Open()
    {
        if (leftDoor == null) return;
        leftDoor.localRotation = Quaternion.Euler(0, leftDoorOpenAngle, 0);
        rightDoor.localRotation = Quaternion.Euler(0, rightDoorOpenAngle, 0);
    }

    public void Close()
    {
        leftDoor.localRotation = Quaternion.identity;
        rightDoor.localRotation = Quaternion.identity;
    }
}
