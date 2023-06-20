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
        _panel.DOFade(1, 0.5f);
        Invoke("Resume", 3.5f);
    }

    private void Resume()
    {
        PlayerStatManager.Instance.Init();
        _playerTrm.transform.position = new Vector3(_startPos.transform.position.x, _startPos.transform.position.y - 0.08f, _startPos.transform.position.z);
        _controller.Gravity = -12;
        _controller.isZeroGravity = false;
        _controller.zeroGravity = 0.01f;
        _controller.Grounded = true;
        _controller.DisableGlider();
        _panel.DOFade(0, 0.5f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }
}
