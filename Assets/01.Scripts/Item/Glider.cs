using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glider : MonoBehaviour
{
    private Animator _animator;

    private readonly int isOnID = Animator.StringToHash("isOn");
    private readonly int isOffID = Animator.StringToHash("isOff");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Enable()
    {
        _animator.SetBool(isOnID, true);
        _animator.SetBool(isOffID, false);
    }

    public void Disable()
    {
        _animator.SetBool(isOffID, true);
        _animator.SetBool(isOnID, false);
    }
}
