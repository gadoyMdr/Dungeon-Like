using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshProUGUI timerText;

    public float time;

    private void Update()
    {
        time += Time.deltaTime;
        double timeDouble = Math.Round(time, 3, MidpointRounding.AwayFromZero);
        timerText.text = $"Time : {timeDouble}";
    }
}
