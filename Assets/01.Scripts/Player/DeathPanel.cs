using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using StarterAssets;

public class DeathPanel : MonoBehaviour
{
    private Image _panel;

    private Transform _playerTrm;
    [SerializeField] private Transform _startPos;

    private FirstPersonController _controller;

    private void Start()
    {
        _controller = GameObject.Find("Player").GetComponent<FirstPersonController>();
        _playerTrm = _controller.gameObject.transform;
        _panel = GetComponent<Image>();
        //_panel.color = new Color(0, 0, 0, 0);
    }

    public void Enabled()
    {
        _panel.DOFade(1, 1.8f).OnComplete(() =>
        {
            Resume();
        });
        
    }

    private void Resume()
    {
        PlayerStatManager.Instance.Init();
        _controller.Grounded = true;
        _playerTrm.transform.position = new Vector3(_startPos.transform.position.x, _startPos.transform.position.y - 0.25f, _startPos.transform.position.z);
        _controller.isZeroGravity = false;
        _controller.DisableGlider();
        _panel.DOFade(0, 0.5f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }
}
