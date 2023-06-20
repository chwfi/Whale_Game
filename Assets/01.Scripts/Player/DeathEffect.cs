using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DeathEffect : MonoBehaviour
{
    private Volume _volume;
    public Vignette vignette;

    public float transitionDuration = 1f;

    public float returnDuration = 1f;

    [SerializeField] private float startIntensity = 0.2f;
    private float targetIntensity = 1f;
    [SerializeField] private float currentIntensity = 0.2f;
    private float transitionTimer = 0f;

    public bool canEffect = false;

    private void Start()
    {
        _volume = GetComponent<Volume>();
        _volume.profile.TryGet<Vignette>(out vignette);
        vignette.intensity.value = startIntensity; // 시작 강도로 초기화
    }

    private void Update()
    {
        if (canEffect)
        {
            transitionTimer += Time.deltaTime;

            if (transitionTimer < transitionDuration)
            {
                float t = transitionTimer / transitionDuration;
                currentIntensity = Mathf.Lerp(startIntensity, targetIntensity, t);
                vignette.intensity.value = currentIntensity;
            }
            else
            {
                vignette.intensity.value = targetIntensity;
                transitionTimer = 0;
                currentIntensity = 0.2f;
                canEffect = false;
            }
        }
        else
        {
            canEffect = false;
            transitionTimer = 0;
            currentIntensity = 0.2f;
            vignette.intensity.value = 0.2f;
        }
            
    }
}
