using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using StarterAssets;

public class ItemManager : MonoBehaviour
{
    public ItemSO Item;

    [SerializeField] private float _collectCooltime = 0.5f;
    [SerializeField] private float _textMaxdis = 15f;
    [SerializeField] private float _mineMaxdis = 5f;

    [SerializeField] private TextMeshPro _text;
    [SerializeField] private TextMeshPro _name;

    public bool CanMining = false;

    float _timer;
    [SerializeField] private float _maxTimer = 5f;
    [SerializeField] private float _miningSpeed = 2f;

    private FirstPersonController _player;

    [SerializeField] private float _zPosRandMinValue = 100f;
    [SerializeField] private float _zPosRandMaxValue = 1200f;

    MeshRenderer _rend;

    private void Start()
    {
        _timer = _maxTimer;
        _player = GameObject.Find("Player").GetComponent<FirstPersonController>();
        _rend = GetComponent<MeshRenderer>();

        float rotRand = Random.Range(-90, 90);
        float posXRand = Random.Range(-60, 60);
        float posYRand = Random.Range(20, 80);
        float posZRand = Random.Range(_zPosRandMinValue, _zPosRandMaxValue);
        this.transform.rotation = Quaternion.Euler(new Vector3(rotRand, rotRand, rotRand));
        this.transform.position = new Vector3(posXRand, posYRand, posZRand);
    }

    void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        this._rend.enabled = false;
    }

    private void Update()
    {
        ResourceMining();
    }

    private void ResourceMining()
    {
        if (Vector3.Distance(transform.position, _player.gameObject.transform.position) <= _textMaxdis)
        {
            UIManager.Instance.ShowInfo(_text, _name);

            if (Vector3.Distance(transform.position, _player.gameObject.transform.position) <= _mineMaxdis && Input.GetKeyDown(KeyCode.F))
                StartCoroutine(CollectResoruces());
        }
        else
            UIManager.Instance.OffInfo(_text, _name);
    }

    public void SetText()
    {
        UIManager.Instance.ShowInfo(_text, _name);
        StartCoroutine(Cooltime());
    }

    private IEnumerator Cooltime()
    {
        yield return new WaitForSeconds(2f);
        UIManager.Instance.OffInfo(_text, _name);
    }

    private IEnumerator CollectResoruces()
    {
        _player.PickupAnimation();
        yield return new WaitForSeconds(_collectCooltime);
        InventoryManager.Instance.Add(Item);
        Destroy(this.gameObject);
    }
}
