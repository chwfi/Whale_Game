using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class WhaleMove : MonoBehaviour
{
    public float whaleMoveSpeed = 2.5f;
    public float minSpeed = 1f;
    [SerializeField]
    private TextMeshProUGUI _kmhText;

    public void SetSpeed(float speed)
    {
        whaleMoveSpeed = speed * 10;
    }

    private void Start()
    {
        whaleMoveSpeed = 2;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * whaleMoveSpeed * Time.deltaTime);
        _kmhText.text = (whaleMoveSpeed * 10).ToString("F0") + "Km/h";

        FuelSystem.Instance.Gauge -= Time.unscaledDeltaTime * whaleMoveSpeed;

        if (FuelSystem.Instance.Gauge <= 0)
        {
            //whaleMoveSpeed = Mathf.Lerp(whaleMoveSpeed, 0, 12);
            FuelSystem.Instance.SetGauge(0);
            SetSpeed(0);
            UIManager.Instance.SpeedSlider.enabled = false;
            UIManager.Instance.SpeedSlider.value = 0;
        }
        else
            UIManager.Instance.SpeedSlider.enabled = true;

        if (whaleMoveSpeed <= minSpeed)
        {
            whaleMoveSpeed = minSpeed;
        }

        if (Input.GetKeyDown(KeyCode.J)) //임시 테스트 코드
        {
            FuelSystem.Instance.Gauge += 70;
        }
    }
}
