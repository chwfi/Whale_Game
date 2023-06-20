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

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _backAudioSource.clip = _backgroundMusic;
        _backAudioSource.Play();
    }

    public void OnHover()
    {
        _effectAudioSource.PlayOneShot(_clips[0]);
    }

    public void OnClick()
    {
        _effectAudioSource.PlayOneShot(_clips[1]);
    }
}
