using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private SphereCollider _coll;

    private void Start()
    {
        _coll = GetComponentInChildren<SphereCollider>();
    }
}
