using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using StarterAssets;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleSO Obstacle;

    private FirstPersonController _player;
    [SerializeField] private float _collectCooltime = 0.5f;
    [SerializeField] private float _textMaxdis = 15f;
    [SerializeField] private float _mineMaxdis = 5f;

    [SerializeField] private float _checkDistance = 50f;

    [SerializeField] private TextMeshPro _text;
    [SerializeField] private TextMeshPro _name;
    [SerializeField] private TextMeshPro _count;

    [SerializeField] private float _zPosRandMinValue = 100f;
    [SerializeField] private float _zPosRandMaxValue = 1200f;
    [SerializeField] private float _xPosRandMinValue;
    [SerializeField] private float _xPosRandMaxValue;
    [SerializeField] private float _yPosRandMinValue;
    [SerializeField] private float _yPosRandMaxValue;

    private float _posXRand;
    private float _posYRand;
    private float _posZRand;

    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private ParticleSystem _destroyParticle;

    Rigidbody _rigid;
    ObjectFadeOut _objectFadeOut;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<FirstPersonController>();

        _objectFadeOut = GetComponentInChildren<ObjectFadeOut>();
        _rigid = GetComponent<Rigidbody>();
        _particle = GetComponentInChildren<ParticleSystem>();
        _posXRand = Random.Range(_xPosRandMinValue, _xPosRandMaxValue);
        _posYRand = Random.Range(_yPosRandMinValue, _yPosRandMaxValue);
        _posZRand = Random.Range(_zPosRandMinValue, _zPosRandMaxValue);
        float rotRand = Random.Range(-90, 90);
        this.transform.rotation = Quaternion.Euler(new Vector3(rotRand, rotRand, rotRand));
        this.transform.position = new Vector3(_posXRand, _posYRand, _posZRand);
    }

    private void Update()
    {
        ResourceMining();

        _text.text = "가까이서 F키를 눌러\n폭발물 부착하여 제거\n" + "현재 폭발물 개수 : " + InventoryManager.Instance.ExplosiveCount.ToString();
    }

    private void ResourceMining()
    {
        if (Vector3.Distance(transform.position, _player.gameObject.transform.position) <= _textMaxdis)
        {
            UIManager.Instance.ShowInfo(_text, _name);

            if (Vector3.Distance(transform.position, _player.gameObject.transform.position) <= _mineMaxdis && Input.GetKeyDown(KeyCode.F))
            {
                if (InventoryManager.Instance.ExplosiveCount >= 1)
                {
                    StartCoroutine(CollectResoruces());
                }
            }          
        }
        else
            UIManager.Instance.OffInfo(_text, _name);
    }

    private IEnumerator CollectResoruces()
    {
        _player.PickupAnimation(true);
        InventoryManager.Instance.ExplosiveCount -= 1;
        yield return new WaitForSeconds(_collectCooltime);
        StartCoroutine(SetTimer());
    }

    private IEnumerator SetTimer()
    {
        while (true)
        {
            for (int i = 5; i > 0; i--)
            {
                _text.text = $"{i}초 후 폭발합니다.";
                yield return new WaitForSeconds(1f);
            }

            SoundManager.Instance.OnCrash();
            _objectFadeOut.FadeOut();
            _destroyParticle.Play();
            //Invoke("DestroyObj", 1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Whale"))
        {
            DurabilitySystem.Instance.Decrease(Obstacle.Damage);
            CameraShake.Instance.HitShake();
            _particle.Play();
            SoundManager.Instance.OnCrash();
            Invoke("Destroy", 2f);
        }
    }

    private void Destroy()
    {
        _particle.Stop();
    }

    private void DestroyObj()
    {
        _text.text = "";
        _name.text = "";
        _destroyParticle.Stop();
        Destroy(this.gameObject);
    }
}
