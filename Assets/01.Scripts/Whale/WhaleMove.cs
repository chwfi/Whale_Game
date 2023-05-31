using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleMove : MonoBehaviour
{
    [SerializeField]
    private float whaleMoveSpeed = 2.5f;

    Rigidbody _rigid;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * whaleMoveSpeed * Time.deltaTime);
    }

    //private void FixedUpdate()
    //{
    //    _rigid.MovePosition(_rigid.position + Vector3.forward * whaleMoveSpeed * Time.deltaTime);
    //}
}
