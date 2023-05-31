using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class RopeSystem : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private ThirdPersonController _controller;

    [SerializeField] private Transform _originPos;
    [SerializeField] private Transform _startPos;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _controller = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        OnRope();

        if (_controller.isZeroGravity)
            _lineRenderer.enabled = true;
        else
            _lineRenderer.enabled = false;
    }

    private void OnRope()
    {
        _lineRenderer.SetPosition(0, _originPos.position);
        _lineRenderer.SetPosition(1, _startPos.position);
    }
}
