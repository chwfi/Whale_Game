using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurabilitySystem : MonoBehaviour
{
    public static DurabilitySystem Instance;
    public float CurrentDurability = 0f;
    public float MaxDurability = 500f;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple Manager is running");
        }
        Instance = this;
    }

    private void Start()
    {
        SetDurability(0);
    }

    public void SetDurability(float durab)
    {
        CurrentDurability = durab;
    }

    public void Decrease(float durab)
    {
        CurrentDurability -= durab;
    }

    public void PlusDutability(float value)
    {
        if (InventoryManager.Instance.CooperIngotCount >= 1 && InventoryManager.Instance.TitanumIngotCount >= 1 && InventoryManager.Instance.BatteryCount >= 1)
        {
            CurrentDurability += value;
            InventoryManager.Instance.CooperIngotCount -= 1;
            InventoryManager.Instance.TitanumIngotCount -= 1;
            InventoryManager.Instance.MaxCooperCount -= 1;
            InventoryManager.Instance.MaxTitanumCount -= 1;
            if (InventoryManager.Instance.MaxBatteryCount > 0)
            {
                InventoryManager.Instance.MaxBatteryCount -= 1;
                InventoryManager.Instance.BatteryCount -= 1;
            }
        }

        if (CurrentDurability >= MaxDurability)
        {
            CurrentDurability = MaxDurability;
        }
    }
}
