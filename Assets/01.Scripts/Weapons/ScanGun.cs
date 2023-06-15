using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using StarterAssets;

public class ScanGun : MonoBehaviour
{
    [SerializeField] private Transform _originPos;
    [SerializeField] private float _showDistance;
    [SerializeField] private float _miningDistance;

    private RaycastHit hit;

    ItemManager _itemPickup;

    public bool CanShoot = true;

    public Camera playerCamera;

    private void Update()
    {
        if (!CanShoot) return;
        ShootRay();
    }

    public void ShootRay()
    {
        if (Physics.Raycast(_originPos.transform.position, _originPos.transform.forward, out hit, _showDistance))
        {
            if (hit.collider.gameObject.CompareTag("Rock"))
            {
                _itemPickup = hit.collider.gameObject.GetComponent<ItemManager>();
                _itemPickup.SetText();
                _itemPickup.CanMining = true;
            }
        }
    }
}