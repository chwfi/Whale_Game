using Cinemachine;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CameraWheel : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float zoomSpeed = 1.0f;
    public float minZoomDistance = 1.0f;
    public float maxZoomDistance = 10.0f;

    void Update()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scrollDelta) > 0.0f)
        {
            float newDistance = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance - (scrollDelta * zoomSpeed);
            newDistance = Mathf.Clamp(newDistance, minZoomDistance, maxZoomDistance);
            virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = newDistance;
        }
    }
}
