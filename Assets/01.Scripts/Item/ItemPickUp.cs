using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using StarterAssets;

public class ItemPickUp : MonoBehaviour
{
    public ItemSO Item;

    [SerializeField] private float _collectCooltime = 0.5f;
    [SerializeField] private float _maxDis = 15f;

    [SerializeField] private TextMeshPro _text;
    [SerializeField] private TextMeshPro _name;

    public bool CanMining = false;

    float _timer;
    [SerializeField] private float _maxTimer = 5f;
    [SerializeField] private float _miningSpeed = 2f;

    private ScanGun _scanGun;
    private FirstPersonController _player;

    public bool isBig;

    [SerializeField] private Rigidbody[] _rigid;

    MeshRenderer _rend;

    private void Start()
    {
        _timer = _maxTimer;
        _scanGun = GameObject.Find("Player").GetComponent<ScanGun>();
        _player = GameObject.Find("Player").GetComponent<FirstPersonController>();
        _rend = GetComponent<MeshRenderer>();

        float rotRand = Random.Range(-90, 90);
        float posXRand = Random.Range(-60, 60);
        float posYRand = Random.Range(25, 120);
        float posZRand = Random.Range(100, 1200);
        this.transform.rotation = Quaternion.Euler(new Vector3(rotRand, rotRand, rotRand));
        this.transform.position = new Vector3(posXRand, posYRand, posZRand);

        if (Item.value >= 3) isBig = true;
        else if (Item.value <= 1) isBig = false;
    }

    void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        this._rend.enabled = false;
        for (int i = 0; i < _rigid.Length; i++)
        {
            _rigid[i].isKinematic = false;
        }
    }

    private void Update()
    {
        BigResourceMining();
        SmallResourceMining();
    }

    private void BigResourceMining()
    {
        if (!isBig) return;
        if (!CanMining) return;

        if (Input.GetMouseButton(0))
        {
            _timer -= Time.unscaledDeltaTime * _miningSpeed;
            if (_timer <= 0)
            {
                StartCoroutine(CollectBigResoruces());           
                _timer = _maxTimer;
            }
        }
        else
            _timer = _maxTimer;
    }

    private void SmallResourceMining()
    {
        if (isBig) return;

        if (Vector3.Distance(transform.position, _player.gameObject.transform.position) <= _maxDis)
        {
            UIManager.Instance.ShowInfo(_text, _name);

            if (Input.GetKeyDown(KeyCode.F))
                StartCoroutine(CollectSmallResoruces());
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

    private IEnumerator CollectBigResoruces()
    {
        yield return new WaitForSeconds(_collectCooltime);
        _scanGun._lineRenderer.enabled = false;
        _scanGun._hitEffect.enabled = false;
        Pickup();
        yield return new WaitForSeconds(5.5f);
        Destroy(this.gameObject);
    }

    private IEnumerator CollectSmallResoruces()
    {
        _player.PickupAnimation();
        yield return new WaitForSeconds(_collectCooltime);
        InventoryManager.Instance.Add(Item);
        Destroy(this.gameObject);
    }
}
