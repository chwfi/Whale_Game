using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private int _animIDPickup;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animIDPickup = Animator.StringToHash("Pickup");
    }

    private void EndPickUp()
    {
        _animator.SetBool(_animIDPickup, false);
    }
}
