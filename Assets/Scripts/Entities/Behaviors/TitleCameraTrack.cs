using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TitleCameraTrack : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Camera camera = Camera.main;
        if (camera.TryGetComponent(out CinemachineBrain brain) == false)
        {
            camera.AddComponent<CinemachineBrain>();
        }
    }
}
