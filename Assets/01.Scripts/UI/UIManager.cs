using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _outInfo;
    [SerializeField] private TextMeshProUGUI _keyInfo;
    public Slider FuelSlider;
    public Slider SpeedSlider;

    [Header("Component")]
    [SerializeField] private WarningText _warnText;

    [Header("Value")]
    [SerializeField] private float _fadeSpeed = 0.8f;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple UIManager is running");
        }
        Instance = this;
    }

    private void Update()
    {
        FuelSlider.value = FuelSystem.Instance.Gauge / FuelSystem.Instance.MaxFuel;
    }

    public void ShowWarningText(bool value)
    {
        _warnText.gameObject.SetActive(value);
    }

    public void ShowInfo(TextMeshPro text, TextMeshPro name)
    {
        text.DOFade(1, _fadeSpeed);
        name.DOFade(1, _fadeSpeed);
    }

    public void OffInfo(TextMeshPro text, TextMeshPro name)
    {
        text.DOFade(0, _fadeSpeed);
        name.DOFade(0, _fadeSpeed);
    }

    public void InSpaceInfo()
    {
        _outInfo.DOFade(1, _fadeSpeed);
        _keyInfo.DOFade(1, _fadeSpeed);
    }

    public void OffSpaceInfo()
    {
        _outInfo.DOFade(0, _fadeSpeed);
        _keyInfo.DOFade(0, _fadeSpeed);
    }
}
