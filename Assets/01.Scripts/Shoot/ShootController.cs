using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class ShootController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimCam;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask mouseLayer = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform firePosition;
    [SerializeField] private Transform aimPosition;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, mouseLayer))
        {
            debugTransform.transform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        if (Input.GetMouseButton(1))
        {
            aimCam.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            //thirdPersonController.SetRotateOnMove(false);
            transform.rotation.SetLookRotation(aimPosition.position);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimCam.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            //thirdPersonController.SetRotateOnMove(true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 aimDir = (mouseWorldPosition - firePosition.position).normalized;
            Instantiate(pfBulletProjectile, firePosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
        }
    }
}
