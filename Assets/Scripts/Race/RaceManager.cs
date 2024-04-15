using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaceManager : MonoBehaviourSingleton<RaceManager>
{
    public UnityEvent onPlayerWin;
    public UnityEvent onEnemyWin;

    private bool playerWins = false;
    private bool enemyWins = false;

    // Llamado cuando el jugador llega a la meta
    public void PlayerReachedGoal()
    {
        if (!enemyWins)  // Si el enemigo no ha llegado antes
        {
            playerWins = true;
            CheckRaceResult();
        }
    }

    // Llamado cuando el enemigo llega a la meta
    public void EnemyReachedGoal()
    {
        if (!playerWins)  // Si el jugador no ha llegado antes
        {
            enemyWins = true;
            CheckRaceResult();
        }
    }

    // Verificar el resultado de la carrera
    private void CheckRaceResult()
    {
        if (playerWins)
        {
            Debug.Log("Player wins the race!");
            onPlayerWin.Invoke();
            // Notificar al GameManager general sobre la victoria del jugador
            //GameManager.Instance.LevelWin();
        }
        else if (enemyWins)
        {
            Debug.Log("Game Over! Enemy wins the race!");
            onEnemyWin.Invoke();
            // Notificar al GameManager general sobre la derrota del jugador
            //GameManager.Instance.LevelGameOver();
        }
    }
}