using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;
using Cinemachine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private float _textMaxdis = 25f;

    [SerializeField] private TextMeshPro _text;
    [SerializeField] private TextMeshPro _name;

    [SerializeField] private float _damage = 50f;

    private FirstPersonController _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<FirstPersonController>();

        float posXRand = Random.Range(-6, 6);
        float posYRand = Random.Range(50, 63);
        float posZRand = Random.Range(450, 2500);
        float rotRand = Random.Range(-90, 90);
        this.transform.rotation = Quaternion.Euler(new Vector3(rotRand, rotRand, rotRand));
        this.transform.position = new Vector3(posXRand, posYRand, posZRand);
    }

    private void Update()
    {
        ShowText();
    }

    public void ShowText()
    {
        if (Vector3.Distance(transform.position, _player.gameObject.transform.position) <= _textMaxdis)
            UIManager.Instance.ShowInfo(_text, _name);
        else
            UIManager.Instance.OffInfo(_text, _name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Whale"))
        {
            CameraManager.Instance.ShakeCamera();
            DurabilitySystem.Instance.Decrease(_damage);
            Destroy(this.gameObject);
        }
    }
}

