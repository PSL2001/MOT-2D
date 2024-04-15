using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
    public string nextSceneName;

    public void GoToStartMenu() 
    {
        SceneManager.LoadScene(0);
    }

    public string GetLevelName() 
    {
        return SceneManager.GetActiveScene().name;
    }

    public void RestartLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartLevelOnSeconds(float seconds)
    {
        StartCoroutine(WaitSecondsCoroutine(seconds));
    }
    private IEnumerator WaitSecondsCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        RestartLevel();
    }

    private IEnumerator RestartLevelOnKeyPress(KeyCode key)
    {
        bool done = false;
        while (!done) // essentially a "while true", but with a bool to break out naturally
        {
            if (Input.GetKeyDown(key))
            {
                done = true; // breaks the loop
                RestartLevel();
            }
            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }

        // now this function returns
    }

    public void GoToLevel(string name) 
    {
        SceneManager.LoadScene(name);
    }

    public void NextLevel() 
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
