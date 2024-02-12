using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public UnityEvent onWin;
    public UnityEvent onGameOver;

    public void LevelWin(float seconds = 0)
    {
        onWin?.Invoke();
        StartCoroutine(WaitSecondsCoroutine(LevelManager.Instance.NextLevel, seconds));
    }

    public void LevelGameOver(float seconds = 0)
    {
        onGameOver?.Invoke();
        StartCoroutine(WaitSecondsCoroutine(LevelManager.Instance.RestartLevel, seconds));
    }

    private IEnumerator WaitSecondsCoroutine(System.Action action, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        action.Invoke();
    }
}
