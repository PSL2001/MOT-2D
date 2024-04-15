using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerConnector : MonoBehaviour
{
    public void PlayBackgroundSound(AudioClip clip) => AudioManager.Instance?.PlayBackgroundSound(clip);

    public void PlayBackgroundSound(string key) => AudioManager.Instance?.PlayBackgroundSound(key);

    public void PlaySFX2D(AudioClip clip) => AudioManager.Instance?.PlaySFX2D(clip);

    public void PlaySFX(AudioClip clip, Vector3 pos) => AudioManager.Instance?.PlaySFX(clip, pos);

    public void PlaySFX(string key) => AudioManager.Instance?.PlaySFX(key);
}
