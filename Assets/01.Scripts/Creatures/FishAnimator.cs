using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAnimator : MonoBehaviour
{
    private readonly int fastID = Animator.StringToHash("isRun");

    private Animator _animator;
    private FishController _fishController;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _fishController = GetComponent<FishController>();
    }

    private void Update()
    {
        if (_fishController.MoveSpeed > 1.75f) _animator.SetBool(fastID, true);
        else _animator.SetBool(fastID, false);
    }
}
