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
        UIManager.Instance.LockUnlockInventoryUI("???", 0);
        UIManager.Instance.LockUnlockInventoryUI("???", 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) UIManager.Instance.ShowInventoryUI(1);
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
            CooperCount += item.value;
            UIManager.Instance.SetInventoryUI(CooperCount, item.itemName, 0);
        }
        else if (item.id == 2)
        {
            TitanumCount += item.value;
            UIManager.Instance.SetInventoryUI(TitanumCount, item.itemName, 1);
        }
    }
}
