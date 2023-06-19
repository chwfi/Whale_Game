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

    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _table;

    [SerializeField] private GameObject[] _check;

    public bool isShowing = false;

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
            
            if (Input.GetKeyDown(KeyCode.E) && !_check[0].activeInHierarchy && !_check[1].activeInHierarchy)
            {
                if (isShowing)
                {
                    UIManager.Instance.OffInfo(_text, _name);
                    Init();
                }
                else
                {
                    _table.SetActive(false);
                    _inventoryPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(-5, 180, 0)), 0.25f).OnComplete(() =>
                    {
                        isShowing = true;
                        _panel.gameObject.SetActive(true);
                    });
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
        _table.SetActive(true);
        isShowing = false;
        _panel.gameObject.SetActive(false);
        _inventoryPanel.transform.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(-115, 180, 0)), 0.25f);
    }
}
