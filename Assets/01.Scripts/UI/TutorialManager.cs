using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Image _tutorialPanel;
    [SerializeField] private float _waitTime = 4f;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string[] _texts;

    [SerializeField] private IntroCamera _introCam;
    [SerializeField] private Image _skipButton;

    [SerializeField] private Image _fadePanel;
    [SerializeField] private GameObject[] _cams;

    int num = 0;

    private void Start()
    {
        OpenPanel();
    }

    private void Update()
    {
        if (num >= _texts.Length) 
        {
            UIManager.Instance._outInfo.gameObject.SetActive(true);
            UIManager.Instance._keyInfo.gameObject.SetActive(true);
            _skipButton.gameObject.SetActive(false);
            return;
        } 

        _text.text = _texts[num];
    }

    public void OpenPanel()
    {
        _tutorialPanel.rectTransform.DOScaleY(1, 0.5f).OnComplete(() =>
        {
            _tutorialPanel.rectTransform.localScale = new Vector3(1, 1, 1);
            
        });
        Invoke("FoldPanel", _waitTime);
    }

    public void FoldPanel()
    {
        _tutorialPanel.rectTransform.DOScaleY(0, 0.5f).OnComplete(() =>
        {
            num++;
            _tutorialPanel.rectTransform.localScale = new Vector3(1, 0, 1);
        });
        if (num < _texts.Length - 1)
            Invoke("OpenPanel", 2.5f);
    }

    public void Skip()
    {
        num = 7;
        FoldPanel();
        _fadePanel.gameObject.SetActive(true);
        _skipButton.rectTransform.DOScaleY(0, 0.5f).OnComplete(() =>
        {
            _skipButton.gameObject.SetActive(false);
        });

        _fadePanel.DOFade(1, 1.5f).OnComplete(() =>
        {
            _cams[0].SetActive(false);
            _cams[1].SetActive(false);
            UIManager.Instance._outInfo.gameObject.SetActive(true);
            UIManager.Instance._keyInfo.gameObject.SetActive(true);
            _fadePanel.DOFade(0, 1f).OnComplete(() =>
            {
                _fadePanel.gameObject.SetActive(false);
                _introCam.OnNextCam();
            });
        }); 
    }
}
