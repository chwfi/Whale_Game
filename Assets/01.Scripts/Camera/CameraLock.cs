using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class CameraLock : MonoBehaviour
{
    [SerializeField] private GameObject[] _objs;

    private FirstPersonController _controller;

    private void Start()
    {
        _controller = GameObject.Find("Player").GetComponent<FirstPersonController>();
    }

    void Update()
    {
        if (_objs[0].activeInHierarchy || _objs[1].activeInHierarchy || _objs[2].activeInHierarchy)
        {
            _controller.CanRotateCam = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            _controller.CanRotateCam = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }
    }
}
