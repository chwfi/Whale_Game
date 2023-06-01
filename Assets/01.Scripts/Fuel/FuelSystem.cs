using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSystem : MonoBehaviour
{
    public static FuelSystem Instance;
    public float Gauge = 0f;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManager is running");
        }
        Instance = this;
    }

    private void Start()
    {
        SetGauge(1000);
    }

    public void SetGauge(float gauge)
    {
        Gauge = gauge;
    }
}
