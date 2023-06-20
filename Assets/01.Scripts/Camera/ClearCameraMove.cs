using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ClearCameraMove : MonoBehaviour
{
    [SerializeField] private Transform _endPos;
    [SerializeField] private CanvasGroup _panel;

    private void Start()
    {
        transform.DOMove(_endPos.transform.position, 30);

        _panel.DOFade(1, 10);
    }
}
