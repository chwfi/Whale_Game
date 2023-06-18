using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    private FishController _fishController;

    private void Start()
    {
        _fishController = GetComponent<FishController>();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _fishController.MoveSpeed * Time.deltaTime);
    }
}
