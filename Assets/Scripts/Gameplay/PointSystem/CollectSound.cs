using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectSound : MonoBehaviour
{
    public AudioClip audioSFX;

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (audioSource != null)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Sonido de recoleccion aqui");
                //Hago que suene
                //audioSource.Play();
                //audioSource.PlayOneShot(audioSFX);
                AudioSource.PlayClipAtPoint(audioSFX, transform.position);
                
            }
        }
    }
}
