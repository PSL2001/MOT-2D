using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountDownManager : MonoBehaviourSingleton<CountDownManager>
{
    [SerializeField] private Indicator currentTime;
    [SerializeField] private float countDownTime = 60.0f;

    public UnityEvent onTimeOut;

    //Inicializar
    private void Start()
    {
        if (currentTime == null) Debug.Log("CountDownManager should have a Indicator attached");
        RestartTimer();
        currentTime.onValueChange.AddListener(CheckForTimeOut);
    }

    private void Update()
    {
        if (currentTime.CurrentValue > 0.0f)
        {
            currentTime.CurrentValue -= Time.deltaTime;
        }
    }

    public void CheckForTimeOut(float f)
    {
        if (currentTime.CurrentValue <= 0.0f) 
        {
            onTimeOut.Invoke();
        } 
    }

    public void AddToTimer(float time) 
    {
        currentTime.CurrentValue += time;
        //CheckForTimeOut();
    }

    public void RemoveFromTimer(float time)
    {
        currentTime.CurrentValue -= time;
        //CheckForTimeOut();
    }

    private void RestartTimer()
    {
        currentTime.maxValue = countDownTime;
        currentTime.CurrentValue = countDownTime;
    }
}
