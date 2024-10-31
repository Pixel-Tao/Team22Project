using UnityEngine;

public class LookController : MonoBehaviour
{
    private Vector3 lookDirection;
    private float lookSpeed = 5;

    private void Update()
    {
        if (lookDirection != Vector3.zero)
        {
            transform.forward = lookDirection;
        }
    }

    public void Look(Vector3 direction)
    {
        lookDirection = direction.normalized;
    }
}
