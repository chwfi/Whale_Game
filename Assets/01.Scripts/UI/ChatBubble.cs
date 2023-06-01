using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    private Transform _camPos;

    private void Start()
    {
        _camPos = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(_camPos.position);
    }
}
