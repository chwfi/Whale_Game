using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantFishAnimator : MonoBehaviour
{
    private GiantFishController _controller;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponentInParent<GiantFishController>();
    }

    private void Update()
    {
        if (_controller.isAttack)
        {
            _animator.SetBool("Attack", true);
        }
    }

    public void OnAnimationDamage()
    {
        if (_controller.isInRange) PlayerStatManager.Instance.OnDamage();
    }

    public void OnAttackAnimationEnd()
    {
        _controller.isAttack = false;
        _animator.SetBool("Attack", false);
    }
}
