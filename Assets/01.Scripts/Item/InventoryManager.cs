using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public int FishCount;

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
        FishCount += item.Value;   
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
    }

    private void Update()
    {
        UIManager.Instance.ShowFuelCountUI(FuelCount);
    }
}
