using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _backAudioSource;
    [SerializeField] private AudioSource _effectAudioSource;
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private AudioClip _backgroundMusic;

    [SerializeField] private float _howlingMinValue;
    [SerializeField] private float _howlingMaxValue;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _backAudioSource.clip = _backgroundMusic;
        _backAudioSource.Play();

        StartCoroutine(HowlingSound());
    }

    private IEnumerator HowlingSound()
    {
        for (int i = 0; i < 100; i++)
        {
            _effectAudioSource.PlayOneShot(_clips[3]);
            yield return new WaitForSeconds(Random.Range(_howlingMinValue, _howlingMaxValue));
        }
    }

    public void OnHover()
    {
        _effectAudioSource.PlayOneShot(_clips[0]);
    }

    public void OnClick()
    {
        _effectAudioSource.PlayOneShot(_clips[1]);
    }

    public void OnPickUp()
    {
        _effectAudioSource.PlayOneShot(_clips[2]);
    }

    public void OnPopup()
    {
        _effectAudioSource.PlayOneShot(_clips[4]);
    }

    public void EngineOnAndRun()
    {
        _effectAudioSource.PlayOneShot(_clips[5]);
    }

    public void EngineOff()
    {
        _effectAudioSource.Stop();
        _effectAudioSource.PlayOneShot(_clips[6]);
    }

    public void EatFood()
    {
        _effectAudioSource.PlayOneShot(_clips[7]);
    }

    public void DrinkWater()
    {
        _effectAudioSource.PlayOneShot(_clips[8]);
    }
}
