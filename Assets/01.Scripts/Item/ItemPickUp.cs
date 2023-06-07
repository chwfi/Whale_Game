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
    [SerializeField] private float _maxDistance = 3f;
    private FirstPersonController _player;
    float _dis;

    [SerializeField] private TextMeshPro _text;
    [SerializeField] private TextMeshPro _name;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<FirstPersonController>();
        
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
        _dis = Vector3.Distance(transform.position, _player.gameObject.transform.position);

        if (_dis <= _maxDistance)
        {
            UIManager.Instance.ShowInfo(_text, _name);
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(CollectResoruces());
                _player.PickupAnimation();
            }      
        }
        else
            UIManager.Instance.OffInfo(_text, _name);
    }

    private IEnumerator CollectResoruces()
    {
       // _player.PickupAnimation();
        yield return new WaitForSeconds(_collectCooltime);
        Pickup();
    }
}
