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
    [SerializeField] private float _xMinValue = -50;
    [SerializeField] private float _xMaxValue = 50;
    [SerializeField] private float _xLimit = 20;
    [SerializeField] private float _yPosRandMinValue = 20f;
    [SerializeField] private float _yPosRandMaxValue = 80f;

    private void Start()
    {
        bool isVaild = false;
        _timer = _maxTimer;
        _player = GameObject.Find("Player").GetComponent<FirstPersonController>();

        float rotRand = Random.Range(-90, 90);

        float posXRand = 0;

        while (!isVaild)
        {
            float randomX = Random.Range(_xMinValue, _xMaxValue);

            if (randomX < -_xLimit || randomX > _xLimit)
            {
                isVaild = true;
                posXRand = randomX;
            }
            else
            {
                isVaild = false;
            }
        }
        float posYRand = Random.Range(_yPosRandMinValue, _yPosRandMaxValue);
        float posZRand = Random.Range(_zPosRandMinValue, _zPosRandMaxValue);
        this.transform.rotation = Quaternion.Euler(new Vector3(rotRand, rotRand, rotRand));
        this.transform.position = new Vector3(posXRand, posYRand, posZRand);
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
        _player.PickupAnimation(true);
        yield return new WaitForSeconds(_collectCooltime);
        SoundManager.Instance.OnPickUp();
        InventoryManager.Instance.Add(Item);
        Destroy(this.gameObject);
    }
}
