using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantFishMovement : MonoBehaviour
{
    private GiantFishController _controller;

    private void Start()
    {
        _controller = GetComponent<GiantFishController>();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _controller.MoveSpeed * Time.deltaTime);
    }
}
