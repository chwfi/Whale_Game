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
        SetDurability(MaxDurability);
    }

    public void SetDurability(float durab)
    {
        CurrentDurability = durab;
    }

    public void Decrease(float durab)
    {
        CurrentDurability -= durab;
    }
}
