using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductManager : MonoBehaviour
{
    [SerializeField] private GameObject _button01;
    [SerializeField] private GameObject _button02;
    [SerializeField] private GameObject _button03;
    [SerializeField] private GameObject _button04;

    public void Product(int productNum)
    {
        if (productNum == 1)
        {
            if (InventoryManager.Instance.CooperCount >= 3)
            {
                InventoryManager.Instance.CooperCount -= 3;
                InventoryManager.Instance.CooperIngotCount += 1;
            }
            else
            {
                UIManager.Instance.ShowProductWarnText(true);
                Invoke("DisableWarning", 1f);
            }
        }

        if (productNum == 2)
        {
            if (InventoryManager.Instance.TitanumCount >= 3)
            {
                InventoryManager.Instance.TitanumCount -= 3;
                InventoryManager.Instance.TitanumIngotCount += 1;
            }
            else
            {
                UIManager.Instance.ShowProductWarnText(true);
                Invoke("DisableWarning", 1f);
            }
        }

        if (productNum == 3)
        {
            if (InventoryManager.Instance.CooperIngotCount >= 2 && InventoryManager.Instance.TitanumIngotCount >= 1)
            {
                InventoryManager.Instance.CooperIngotCount -= 2;
                InventoryManager.Instance.TitanumIngotCount -= 1;
                InventoryManager.Instance.SolutionCount += 1;
            }
            else
            {
                UIManager.Instance.ShowProductWarnText(true);
                Invoke("DisableWarning", 1f);
            }
        }

        if (productNum == 4)
        {
            if (InventoryManager.Instance.SolutionCount >= 2 && InventoryManager.Instance.PlasticBottleCount >= 1)
            {
                InventoryManager.Instance.SolutionCount -= 2 ;
                InventoryManager.Instance.PlasticBottleCount -= 1;
                InventoryManager.Instance.FuelCount += 1;
            }
            else
            {
                UIManager.Instance.ShowProductWarnText(true);
                Invoke("DisableWarning", 1f);
            }
        }

        if (productNum == 5)
        {
            if (InventoryManager.Instance.TitanumIngotCount >= 9 && InventoryManager.Instance.BatteryCount >= 1)
            {
                InventoryManager.Instance.TitanumIngotCount -= 9;
                InventoryManager.Instance.BatteryCount -= 1;
                InventoryManager.Instance.LowTankCount += 1;
                _button01.SetActive(false);
            }
            else
            {
                UIManager.Instance.ShowProductWarnText(true);
                Invoke("DisableWarning", 1f);
            }
        }

        if (productNum == 6)
        {
            if (InventoryManager.Instance.TitanumIngotCount >= 24 && InventoryManager.Instance.BatteryCount >= 2)
            {
                InventoryManager.Instance.TitanumIngotCount -= 24;
                InventoryManager.Instance.BatteryCount -= 2;
                InventoryManager.Instance.HighTankCount += 1;
                _button02.SetActive(false);
            }
            else
            {
                UIManager.Instance.ShowProductWarnText(true);
                Invoke("DisableWarning", 1f);
            }
        }

        if (productNum == 7)
        {
            if (InventoryManager.Instance.CooperIngotCount >= 12 && InventoryManager.Instance.TitanumIngotCount >= 9)
            {
                InventoryManager.Instance.CooperIngotCount -= 12;
                InventoryManager.Instance.TitanumIngotCount -= 9;
                InventoryManager.Instance.FlipperCount += 1;
                _button03.SetActive(false);
            }
            else
            {
                UIManager.Instance.ShowProductWarnText(true);
                Invoke("DisableWarning", 1f);
            }
        }

        if (productNum == 8)
        {
            if (InventoryManager.Instance.TitanumIngotCount >= 30 && InventoryManager.Instance.BatteryCount >= 3)
            {
                InventoryManager.Instance.TitanumIngotCount -= 30;
                InventoryManager.Instance.BatteryCount -= 3;
                InventoryManager.Instance.GliderCount += 1;
                _button04.SetActive(false);
            }
            else
            {
                UIManager.Instance.ShowProductWarnText(true);
                Invoke("DisableWarning", 1f);
            }
        }

        if (productNum == 9)
        {
            if (InventoryManager.Instance.IceCount >= 1 && InventoryManager.Instance.PlasticBottleCount >= 1)
            {
                InventoryManager.Instance.IceCount -= 1;
                InventoryManager.Instance.PlasticBottleCount -= 1;
                InventoryManager.Instance.WaterCount += 1;
            }
            else
            {
                UIManager.Instance.ShowProductWarnText(true);
                Invoke("DisableWarning", 1f);
            }
        }
    }

    private void DisableWarning()
    {
        UIManager.Instance.ShowProductWarnText(false);
    }
}
