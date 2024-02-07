using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Timer : MonoBehaviour
{
    bool timerActive = false;
    float currentTime;
    public int startMin;
    public TextMeshProUGUI currentTimeText;

    private void Awake()
    {
        StartTimer();
    }

    void Start()
    {
        currentTime = startMin * 60;
    }

    void Update()
    {
        if (timerActive)
        {
            currentTime -= Time.deltaTime; 
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = currentTime.ToString();
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}
