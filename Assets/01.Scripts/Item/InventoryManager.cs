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

    private void Awake()
    {
        Instance = this;
    }

    public void Add(ItemSO item)
    {
        Manage(item);
    }

    public void Remove(ItemSO item)
    {
        Items.Remove(item);
    }

    public void Manage(ItemSO item)
    {
        if (item.id == 1)
        {
            if (CooperCount <= 0)
            {
                CooperCount += item.value;
                UIManager.Instance.SetInventoryUI(item.itemName, 0);
            }
            else
            {
                CooperCount += item.value;
                UIManager.Instance.SetInventoryUI(item.itemName, 0);
            }
        }
        else if (item.id == 2)
        {
            if (TitanumCount <= 0)
            {
                TitanumCount += item.value;
                UIManager.Instance.SetInventoryUI(item.itemName, 1);
            }
            else
            {
                TitanumCount += item.value;
                UIManager.Instance.SetInventoryUI(item.itemName, 1);
            }
        }
        else if (item.id == 5)
        {
            if (PlasticBottleCount <= 0)
            {
                PlasticBottleCount += item.value;
                //UIManager.Instance.SetInventoryUI(item.itemName, 2);
            }
            else
            {
                PlasticBottleCount += item.value;
                //UIManager.Instance.SetInventoryUI(item.itemName, 2);
            }
        }
        else if (item.id == 6)
        {
            CooperCount += item.value;
            TitanumCount += item.value;
        }
    }

    private void Update()
    {
        UIManager.Instance.ShowFuelCountUI(FuelCount);
    }
}
