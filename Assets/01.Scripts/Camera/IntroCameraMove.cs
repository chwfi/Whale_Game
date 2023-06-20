using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class IntroCameraMove : MonoBehaviour
{
    [SerializeField] private Transform _targetTrm;
    [SerializeField] private Transform _destination;

    [SerializeField] private IntroCamera _nextCam;

    private void Start()
    {
        transform.DOMove(_destination.transform.position, 46f).OnComplete(() =>
        {
            _nextCam.DoAction();
            gameObject.SetActive(false);
        });
    }

    private void Update()
    {
        transform.LookAt(_targetTrm);
    }
}
