using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    [SerializeField] private FishSO _fishSO;
    public FishSO FishSO => _fishSO;

    private float _moveSpeed;
    public float MoveSpeed => _moveSpeed;

    private Transform _player;
    [SerializeField] private float _distance;

    private void Start()
    {
        _player = GameObject.Find("Player").transform;
        SetVaule(_fishSO.FishMoveSpeed);
    }

    private void SetVaule(float value)
    {
        _moveSpeed = value;
    }

    private void Update()
    {
        RunState();
    }

    private void RunState()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) <= _distance)
        {
            SetVaule(_fishSO.FishMoveSpeed);
        }
        else
        {
            SetVaule(_fishSO.FishFastMoveSpeed);
        }      
    }
}
