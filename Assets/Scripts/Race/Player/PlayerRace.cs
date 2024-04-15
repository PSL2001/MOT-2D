using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRace : MonoBehaviourSingleton<RaceManager>
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ajusta la etiqueta según tus necesidades
        {
            RaceManager.Instance.PlayerReachedGoal();
        }
    }
}

