using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviourSingleton<ScoreManager>
{
    [SerializeField] private Indicator score;
    [SerializeField] private float winningScore = 10;

    public UnityEvent onWinningScore;

    //Inicialización
    private void Start()
    {
        ResetScore();
       // score.onValueChange.AddListener(CheckWinCondition(score.CurrentValue)); // Alternativa mediante eventos para comprobrar si se ha ganado, comentar la linea 30
    }

    //Actualización
    public void AddPoints(float points) 
    {
        score.CurrentValue += points;
        CheckWinCondition(score.CurrentValue);
    }

    public void CheckWinCondition(float points)
    {
        if (points >= winningScore) 
        {
            onWinningScore.Invoke();
        }
    }

    private void ResetScore()
    {
        score.CurrentValue = 0;
    }
}
