using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using StarterAssets;

public class ShowTextInfo : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 3f;
    private Transform _playerPos;
    float _dis;

    FirstPersonController _controller;

    [SerializeField] private TextMeshPro _text;
    [SerializeField] private TextMeshPro _name;

    [SerializeField] private CanvasGroup _panel;

    private void Awake()
    {
        _playerPos = GameObject.Find("Player").GetComponent<Transform>();
        _controller = GameObject.Find("Player").GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        _dis = Vector3.Distance(transform.position, _playerPos.position);

        if (_dis <= _maxDistance)
        {
            UIManager.Instance.ShowInfo(_text, _name);
            
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (_panel.alpha >= 0.5f)
                {
                    Init();
                }       
                else
                {
                    _panel.DOFade(1, 0.5f);
                    //_controller.LockCameraPosition = true;
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                }    
            }
        }
        else
        {
            UIManager.Instance.OffInfo(_text, _name);
            Init();
        }    
    }

    private void Init()
    {
        _panel.DOFade(0, 0.5f);
        //_controller.LockCameraPosition = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }
}
