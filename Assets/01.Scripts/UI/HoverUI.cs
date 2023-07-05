using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image _button;

    private void Start()
    {
        _button = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _button.rectTransform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _button.rectTransform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
    }

    public void OnEatClick()
    {
        _button.rectTransform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).OnComplete(() =>
        {
            _button.rectTransform.DOScale(new Vector3(1, 1, 1), 0.1f);
        });
    }
}
