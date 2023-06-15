using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSystem : MonoBehaviour
{
    public static FuelSystem Instance;
    public float Gauge = 0f;
    public float MaxFuel = 500f;

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
        SetGauge(MaxFuel);
    }

    public void SetGauge(float gauge)
    {
        Gauge = gauge;
    }
}
