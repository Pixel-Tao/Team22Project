using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAdapter : MonoBehaviour
{
    public float spinVelocity = 0f;
    public bool spinDirectionYAxis = false;

    private float localTimer = 0f;
    private float maxRotAngle = 360f;
    private float deltaScale = 0.02f;

    void Update()
    {
        localTimer += Time.deltaTime;
        if(localTimer > deltaScale)
        {
            this.gameObject.transform.Rotate(spinDirectionYAxis ?
            new Vector3(0f, spinVelocity, 0f):
            new Vector3(0f, 0f, spinVelocity));

            Quaternion rot = this.transform.rotation;
            if (rot.y > maxRotAngle || rot.z > maxRotAngle)
            {
                this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            localTimer = 0f;
        }       
    }
}
