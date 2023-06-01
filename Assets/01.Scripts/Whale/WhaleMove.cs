using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleMove : MonoBehaviour
{
    [SerializeField]
    private float whaleMoveSpeed = 2.5f;

    public void SetSpeed(float speed)
    {
        whaleMoveSpeed = speed * 10;
    }

    private void Start()
    {
        whaleMoveSpeed = 2;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * whaleMoveSpeed * Time.deltaTime);
        FuelSystem.Instance.Gauge -= Time.deltaTime * whaleMoveSpeed;
    }
}
