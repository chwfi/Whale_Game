using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using StarterAssets;

public class ScanGun : MonoBehaviour
{
    [SerializeField] private Transform _originPos;
    [SerializeField] private float _distance;

    private Transform _playerPos;
    public LineRenderer _lineRenderer;
    private RaycastHit hit;

    ItemPickUp _itemPickup;

    public bool canShoot = true;

    public Camera playerCamera;
    public Transform laserOrigin;

    [SerializeField] private VisualEffect _beam;
    public VisualEffect _hitEffect;

    private void Start()
    {
        _playerPos = GetComponent<Transform>();
    }

    private void Update()
    {
        if (!canShoot)
        {
            _hitEffect.Stop();
            _lineRenderer.enabled = false;
            return;
        }
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
                if (!_itemPickup.isBig) return;
                if (Input.GetMouseButton(0))
                {
                    _lineRenderer.SetPosition(0, laserOrigin.position);
                    _lineRenderer.SetPosition(1, hit.point);
                    _hitEffect.enabled = true;
                    _lineRenderer.enabled = true;
                    _hitEffect.transform.position = hit.point;
                }
                else
                {
                    _hitEffect.enabled = false;
                    _lineRenderer.enabled = false;
                    _hitEffect.Play();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _hitEffect.Stop();
            }
        }
    }
}