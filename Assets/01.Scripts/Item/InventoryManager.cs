using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ItemSO> Items = new List<ItemSO>();

    public int CooperCount;
    public int TitanumCount;

    public int CooperIngotCount;
    public int TitanumIngotCount;

    public int SolutionCount;
    public int FuelCount;
    public int PlasticBottleCount;
    public int IceCount;

    public int FishCount;
    public int WaterGunFishCount;

    public int BatteryCount;

    public int LowTankCount;
    public int HighTankCount;
    public int FlipperCount;
    public int GliderCount;

    public int WaterCount;

    public int ExplosiveCount;

    public int MaxFuelCount = 20;

    public int MaxCooperCount = 50;
    public int MaxTitanumCount = 50;
    public int MaxBatteryCount = 10;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(ItemSO item)
    {
        Manage(item);
    }

    public void AddCreature(FishSO fish)
    {
        ManageCreature(fish);
    }

    public void Remove(ItemSO item)
    {
        Items.Remove(item);
    }

    public void ManageCreature(FishSO item)
    {
        if (item.Id == 20)
        {
            SolutionCount += 1;
        }
        else if (item.Id == 10)
        {
            FishCount += item.Value;
        }
        else if (item.Id == 21)
        {
            WaterGunFishCount += item.Value;
        }
    }

    public void Manage(ItemSO item)
    {
        if (item.Id == 1)
        {
            if (CooperCount <= 0)
            {
                CooperCount += item.Value;
            }
            else
            {
                CooperCount += item.Value;
            }
        }
        else if (item.Id == 2)
        {
            if (TitanumCount <= 0)
            {
                TitanumCount += item.Value;
            }
            else
            {
                TitanumCount += item.Value;
            }
        }
        else if (item.Id == 5)
        {
            if (PlasticBottleCount <= 0)
            {
                PlasticBottleCount += item.Value;
                //UIManager.Instance.SetInventoryUI(item.itemName, 2);
            }
            else
            {
                PlasticBottleCount += item.Value;
                //UIManager.Instance.SetInventoryUI(item.itemName, 2);
            }
        }
        else if (item.Id == 6)
        {
            CooperCount += item.Value;
            TitanumCount += item.Value;
        }
        else if (item.Id == 8)
        {
            BatteryCount += item.Value;
        }
        else if (item.Id == 9)
        {
            IceCount += item.Value;
        }
    }

    public void Init()
    {
        CooperCount = 0;
        CooperIngotCount = 0;
        TitanumCount = 0;
        TitanumIngotCount = 0;
        SolutionCount = 0;
        FuelCount = 0;
        PlasticBottleCount = 0;
        IceCount = 0;
        FishCount = 0;
        WaterCount = 0;
        WaterGunFishCount = 0;
        BatteryCount = 0;
    }

    private void Update()
    {
        UIManager.Instance.ShowFuelCountUI(FuelCount, MaxFuelCount);
        UIManager.Instance.ShowDurCountUI(CooperIngotCount, MaxCooperCount, TitanumIngotCount, MaxTitanumCount, BatteryCount, MaxBatteryCount);

        if (MaxBatteryCount <= 0 && MaxCooperCount <= 0 && MaxFuelCount <= 0 && MaxTitanumCount <= 0)
        {
            SceneManager.LoadScene("Clear");
        }
    }
}
