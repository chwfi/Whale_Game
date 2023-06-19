using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;

public class GiantFishController : MonoBehaviour
{
    [SerializeField] private GiantFishSO _giantSo;
    public GiantFishSO GiantSO => _giantSo;

    private float _moveSpeed;
    public float MoveSpeed => _moveSpeed;

    private Transform _player;
    [SerializeField] private float _distance;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _checkDistance;

    public bool isAttack = false;
    public bool isInRange = false;

    [SerializeField] private float _attackCoolTime = 3f;

    [SerializeField] private TextMeshPro _text;
    [SerializeField] private TextMeshPro _name;

    private void Start()
    {
        _player = GameObject.Find("Player").transform;
        SetVaule(_giantSo.GiantNormalMoveSpeed);
    }

    private void SetVaule(float value)
    {
        _moveSpeed = value;
    }

    private void Update()
    {
        AttackState();

        LookState();

        if (Vector3.Distance(transform.position, _player.transform.position) <= _distance + 35f)
        {
            UIManager.Instance.ShowInfo(_text, _name);
        }
        else
        {
            UIManager.Instance.OffInfo(_text, _name);
        }
    }

    private void AttackState()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) <= _attackDistance)
        {
            _attackCoolTime -= Time.deltaTime;
            if (_attackCoolTime <= 0)
            {
                isAttack = true;
                _attackCoolTime = 3;
            }
            else
            {
                isAttack = false;
            }
        }

        if (Vector3.Distance(transform.position, _player.transform.position) <= _checkDistance)
            isInRange = true;
        else
            isInRange = false;
    }

    private void LookState()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) <= _distance)
        {
            SetVaule(_giantSo.GiantFastMoveSpeed);
            _distance = 65f;
            transform.DOLookAt(_player.transform.position, 3.5f).OnComplete(() =>
            {
                transform.LookAt(_player.transform.position);
            });
        }
        else
        {
            _distance = 50f;
            transform.DOLookAt(new Vector3(0, 0, 2000f), 6f);
            SetVaule(_giantSo.GiantNormalMoveSpeed);
        }      
    }
}
