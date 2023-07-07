using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningBar : MonoBehaviour
{
    private Transform _whaleTrm;
    [SerializeField] private Transform _asteroidTrm;

    [SerializeField] private float _checkDistance;

    private void Start()
    {
        _whaleTrm = GameObject.Find("Biomechanical Whale Swimming").transform;
    }


    void Update()
    {
        if (Vector3.Distance(_whaleTrm.transform.position, _asteroidTrm.transform.position) <= _checkDistance)
        {
            UIManager.Instance.ShowWarningText02(1);
        }
        else
            UIManager.Instance.ShowWarningText02(0);

    }
}
