using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private Image _panel;

    public void NextScene()
    {
        _panel.DOFade(1, 1f).OnComplete(() =>
        {
            SceneManager.LoadScene("Game");
        });     
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToHome()
    {
        SceneManager.LoadScene("Start");
    }
}
