using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IntroCamera : MonoBehaviour
{
    private CinemachineVirtualCamera _cam;
    private Animator _animator;

    [SerializeField] GameObject _panel;
    [SerializeField] TutorialManager _tutorial;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _cam = GetComponent<CinemachineVirtualCamera>();
    }

    public void DoAction()
    {
        _cam.Priority = 13;
        _animator.SetTrigger("On");
    }

    public void OnNextCam()
    {
        _panel.SetActive(true);
        //_tutorial.OpenPanel();
        _cam.Priority = 0;
        gameObject.SetActive(false);
    }
}
