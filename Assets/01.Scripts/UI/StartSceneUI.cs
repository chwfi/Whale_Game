using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartSceneUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image _button;

    private CanvasGroup _settingPanel;
    private SoundManager _soundManager;

    private void Start()
    {
        _settingPanel = GameObject.Find("SettingPanel").GetComponent<CanvasGroup>();
        _soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        _button = GetComponent<Image>();    
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _soundManager.OnHover();
        _button.rectTransform.DOSizeDelta(new Vector2(650, 111), 0.1f);
        _button.rectTransform.DOAnchorPosX(-400, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _button.rectTransform.DOSizeDelta(new Vector2(485, 111), 0.1f);
        _button.rectTransform.DOAnchorPosX(-485, 0.1f);
    }

    public void OffSetting()
    {
        _soundManager.OnClick();
        _settingPanel.DOFade(0, 0.3f);
    }

    public void OnSetting()
    {
        _soundManager.OnClick();
        _settingPanel.DOFade(1, 0.3f);
    }
}
