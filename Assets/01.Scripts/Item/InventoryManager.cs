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
                UIManager.Instance.SetInventoryUI(CooperCount, item.itemName, 0);
            }
            else
            {
                CooperCount += item.value;
                UIManager.Instance.SetInventoryUI(CooperCount, item.itemName, 0);
            }
        }
        else if (item.id == 2)
        {
            if (TitanumCount <= 0)
            {
                TitanumCount += item.value;
                UIManager.Instance.SetInventoryUI(TitanumCount, item.itemName, 1);
            }
            else
            {
                TitanumCount += item.value;
                UIManager.Instance.SetInventoryUI(TitanumCount, item.itemName, 1);
            }
        }
    }
}
