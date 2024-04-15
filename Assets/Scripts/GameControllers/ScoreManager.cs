using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviourSingleton<ScoreManager>
{
    [SerializeField] public Indicator score;
    //[SerializeField] private float winningScore = 10;

    //public UnityEvent onWinningScore;

    //Inicialización
    private void Start()
    {
        if (score == null) Debug.Log("Score Manager should have a FloatVariableSO attached");
        ResetScore();
        //score.onValueChange.AddListener(); // Alternativa mediante eventos para comprobrar si se ha ganado, comentar la linea 30
    }

    //Actualización
    public void AddPoints(float points) 
    {
        score.CurrentValue += points;
    }

    public void QuitPoints(float points)
    {
        score.CurrentValue -= points;
    }

    //public void CheckWinCondition(float f)
    //{
    //    if (score.CurrentValue >= winningScore) 
    //    {
    //        onWinningScore.Invoke();
    //    }
    //}

    private void ResetScore()
    {
        score.CurrentValue = 0;
    }
}
