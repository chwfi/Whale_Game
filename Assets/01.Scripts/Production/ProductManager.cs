using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ProductManager : MonoBehaviour
{
    [SerializeField] private GameObject _button01;
    [SerializeField] private GameObject _button02;
    [SerializeField] private GameObject _button03;
    [SerializeField] private GameObject _button04;

    [SerializeField] private Sprite[] _equipmentIcons;

    FirstPersonController _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<FirstPersonController>();
    }

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
            if (InventoryManager.Instance.TitanumIngotCount >= 6 && InventoryManager.Instance.CooperIngotCount >= 12)
            {
                InventoryManager.Instance.TitanumIngotCount -= 6;
                InventoryManager.Instance.CooperIngotCount -= 12;
                InventoryManager.Instance.LowTankCount += 1;
                EquipmentManager.Instance.ChangeTopEquipment(_equipmentIcons[0], "저용량 산소탱크");
                PlayerStatManager.Instance.PlayerStats.MaxOxygen += 25;
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
                EquipmentManager.Instance.ChangeTopEquipment(_equipmentIcons[1], "고용량 산소탱크");
                PlayerStatManager.Instance.PlayerStats.MaxOxygen += 50;
                _button02.SetActive(false);
                _button01.SetActive(false);
            }
            else
            {
                UIManager.Instance.ShowProductWarnText(true);
                Invoke("DisableWarning", 1f);
            }
        }

        if (productNum == 7)
        {
            if (InventoryManager.Instance.CooperIngotCount >= 9 && InventoryManager.Instance.TitanumIngotCount >= 6)
            {
                InventoryManager.Instance.CooperIngotCount -= 9;
                InventoryManager.Instance.TitanumIngotCount -= 6;
                InventoryManager.Instance.FlipperCount += 1;
                EquipmentManager.Instance.ChangeLowEquipment(_equipmentIcons[2], "가속 갈퀴");
                _player.MoveSpeed = 6;
                _player.SprintSpeed = 8;
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
                EquipmentManager.Instance.ChangeLowEquipment(_equipmentIcons[3], "글라이더");
                _player.MoveSpeed = 9;
                _player.SprintSpeed = 11;
                _button04.SetActive(false);
                _button03.SetActive(false);
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
