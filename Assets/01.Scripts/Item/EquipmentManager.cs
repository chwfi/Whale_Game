using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;

    [SerializeField] private Image _topEquipment;
    [SerializeField] private Image _lowEquipment;
    [SerializeField] private TextMeshProUGUI _topText;
    [SerializeField] private TextMeshProUGUI _lowText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _topText.text = "없음";
        _lowText.text = "없음";

        _topEquipment.sprite = null;
        _topEquipment.color = new Color(1, 1, 1, 0);
        _lowEquipment.sprite = null;
        _lowEquipment.color = new Color(1, 1, 1, 0);
    }

    public void ChangeTopEquipment(Sprite sprite, string text)
    {
        _topEquipment.color = new Color(1, 1, 1, 1);
        _topEquipment.sprite = sprite;
        _topText.text = text;
    }

    public void ChangeLowEquipment(Sprite sprite, string text)
    {
        _lowEquipment.color = new Color(1, 1, 1, 1);
        _lowEquipment.sprite = sprite;
        _lowText.text = text;
    }
}
