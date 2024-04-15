using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRace : MonoBehaviourSingleton<RaceManager> 
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyRace")) // Ajusta la etiqueta según tus necesidades
        {
            RaceManager.Instance.EnemyReachedGoal();
        }
    }
}
