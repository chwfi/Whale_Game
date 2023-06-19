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
        SetGauge(0);
    }

    public void SetGauge(float gauge)
    {
        Gauge = gauge;
    }

    public void PlusGauge(float gauge)
    {
        if (InventoryManager.Instance.FuelCount >= 1)
        {
            Gauge += gauge;
            InventoryManager.Instance.FuelCount -= 1;
            InventoryManager.Instance.MaxFuelCount -= 1;
        }

        if (Gauge >= MaxFuel)
        {
            Gauge = MaxFuel;
        }
    }
}
