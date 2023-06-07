using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScanGun : MonoBehaviour
{
    [SerializeField] private Transform _originPos;
    [SerializeField] private float _distance;

    private Transform _playerPos;
    private LineRenderer _lineRenderer;
    private RaycastHit hit;

    ItemPickUp _itemPickup;

    public bool canShoot = false;

    public Camera playerCamera;
    public Transform laserOrigin;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _playerPos = GetComponent<Transform>();
    }

    private void Update()
    {
        if (!canShoot) return;
        ShootRay();
    }

    public void ShootRay()
    {
        if (Physics.Raycast(_originPos.transform.position, _originPos.transform.forward, out hit, _distance))
        {
            if (hit.collider.gameObject.CompareTag("Rock"))
            {
                _itemPickup = hit.collider.gameObject.GetComponent<ItemPickUp>();
                _itemPickup.CanMining = true;
                _itemPickup.SetText();
                if (Input.GetMouseButton(0))
                {
                    _lineRenderer.SetPosition(0, laserOrigin.position);
                    _lineRenderer.SetPosition(1, hit.point);
                    _lineRenderer.enabled = true;
                }
                else if (hit.collider.gameObject == null)
                    _lineRenderer.enabled = false;
                else
                {
                    _lineRenderer.enabled = false;
                }
            }
            else
                _itemPickup.CanMining = false;
        }
    }
}