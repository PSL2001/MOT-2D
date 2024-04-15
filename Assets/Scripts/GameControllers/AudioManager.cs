using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    //Propiedades
    [Header("Parametros de configuracion")]
    [SerializeField][Range(0, 1)] private float backgroundSoundVolume = 1;
    [SerializeField][Range(0, 1)] public float SFXVolume = 1;

    //Referencia a Audiosource
    AudioSource audioSource;

    //Background Sound default Value
    [Header("Audios Generales")]
    [SerializeField] private AudioClip audioClip;

    [SerializedDictionary("Key", "Background sound")] public SerializedDictionary<string, AudioClip> audioDictionary;
    [SerializedDictionary("Key", "SFX")] public SerializedDictionary<string, AudioClip> soundDictionary; 
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = backgroundSoundVolume;
        PlayBackgroundSound(audioClip);
    }

    #region Background
    public void PlayBackgroundSound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayBackgroundSound(string key)
    {
        if (audioDictionary.ContainsKey(key))
        {
            PlayBackgroundSound(audioDictionary[key]);
        }
        else
        {
            Debug.LogError($"La clave {key} no existe en Audio Dictionary");
        }
    }
    #endregion
    public void PlaySFX2D(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, SFXVolume); //Instanciamos en la camara
    }

    public void PlaySFX(AudioClip clip, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos, SFXVolume);
    }

    public void PlaySFX(string key)
    {
        if (soundDictionary.ContainsKey(key))
        {
            PlaySFX2D(soundDictionary[key]);
        } else
        {
            Debug.LogError($"La clave {key} no existe en Sound Dictionary");
        }
    }

    private void Awake()
    {
        backgroundSoundVolume = PlayerPrefs.GetFloat("backgroundVolume", 1);
        SFXVolume = PlayerPrefs.GetFloat("sfxVolume", 1);
    }

    private void OnDestroy()
    {
         PlayerPrefs.SetFloat("backgroundVolume", backgroundSoundVolume);
         PlayerPrefs.SetFloat("sfxVolume", SFXVolume);
    }
}
