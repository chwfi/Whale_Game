using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductManager : MonoBehaviour
{
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
    }

    private void DisableWarning()
    {
        UIManager.Instance.ShowProductWarnText(false);
    }
}
