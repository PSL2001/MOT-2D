using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void LevelRestart(float seconds)
    {
        StartCoroutine(WaitSecondsCoroutine(seconds));
        //StartCoroutine(waitForKeyPress(KeyCode.Space));
    }
    private IEnumerator WaitSecondsCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator waitForKeyPress(KeyCode key)
    {
        bool done = false;
        while (!done) // essentially a "while true", but with a bool to break out naturally
        {
            if (Input.GetKeyDown(key))
            {
                done = true; // breaks the loop
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }

        // now this function returns
    }
}
