using UnityEngine;
using DG.Tweening;

public class ObjectFadeOut : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void FadeOut()
    {
        _animator.SetTrigger("Fade");
    }

    public void EndAnimation()
    {
        InventoryManager.Instance.CooperCount += 6;
        InventoryManager.Instance.TitanumCount += 6;
        Destroy(transform.parent.gameObject);
    }
}
