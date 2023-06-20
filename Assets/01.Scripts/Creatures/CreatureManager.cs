using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using StarterAssets;

public class CreatureManager : MonoBehaviour
{
    public FishSO Fish;

    [SerializeField] private float _collectCooltime = 0.5f;
    [SerializeField] private float _textMaxdis = 15f;
    [SerializeField] private float _mineMaxdis = 5f;

    [SerializeField] private TextMeshPro _text;
    [SerializeField] private TextMeshPro _name;

    private FirstPersonController _player;

    [SerializeField] private float _zPosRandMinValue = 100f;
    [SerializeField] private float _zPosRandMaxValue = 1200f;

    [SerializeField] private float _xPosRandMinValue = -75f;
    [SerializeField] private float _xposRandMaxValue = 75f;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<FirstPersonController>();

        float rotRand = Random.Range(_xPosRandMinValue, _xposRandMaxValue);
        float posXRand = Random.Range(-75, 70);
        float posYRand = Random.Range(30, 70);
        float posZRand = Random.Range(_zPosRandMinValue, _zPosRandMaxValue);
        this.transform.rotation = Quaternion.Euler(new Vector3(0, rotRand, 0));
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

    private IEnumerator CollectResoruces()
    {
        _player.PickupAnimation();
        yield return new WaitForSeconds(_collectCooltime);
        InventoryManager.Instance.AddCreature(Fish);
        Destroy(this.gameObject);
    }
}

