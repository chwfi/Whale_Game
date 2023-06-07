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

    [SerializeField] private TextMeshPro _text;
    [SerializeField] private TextMeshPro _name;

    public bool CanMining = false;

    float _timer;
    [SerializeField] private float _maxTimer = 5f;
    [SerializeField] private float _miningSpeed = 2f;

    private void Start()
    {
        _timer = _maxTimer;
        
        float rotRand = Random.Range(-90, 90);
        float posXRand = Random.Range(-60, 60);
        float posYRand = Random.Range(25, 120);
        float posZRand = Random.Range(100, 1200);
        this.transform.rotation = Quaternion.Euler(new Vector3(rotRand, rotRand, rotRand));
        this.transform.position = new Vector3(posXRand, posYRand, posZRand);
    }

    void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (!CanMining) return;

        if (Input.GetMouseButton(0))
        {
            _timer -= Time.unscaledDeltaTime * _miningSpeed;
            if (_timer <= 0)
            {
                StartCoroutine(CollectResoruces());
                _timer = _maxTimer;
            }
        }
        else
            _timer = _maxTimer;
    }

    public void SetText()
    {
        UIManager.Instance.ShowInfo(_text, _name);
        StartCoroutine(Cooltime());
    }

    private IEnumerator Cooltime()
    {
        yield return new WaitForSeconds(1f);
        UIManager.Instance.OffInfo(_text, _name);
    }

    private IEnumerator CollectResoruces()
    {
       // _player.PickupAnimation();
        yield return new WaitForSeconds(_collectCooltime);
        Pickup();
    }
}
