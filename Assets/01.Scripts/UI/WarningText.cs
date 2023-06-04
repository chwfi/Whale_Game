using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class WarningText : MonoBehaviour
{
    public Ease EaseType;
    private TextMeshProUGUI _text;

    public bool canFade = false;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _text.DOFade(0.0f, 1f).SetEase(this.EaseType).SetLoops(-1, LoopType.Yoyo);
    }
}
