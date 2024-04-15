using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;

public class AddPoints : MonoBehaviour
{
    //Datos propios
    [SerializeField] [Range(1, 10)] private int points = 1;

    //Eventos
    public UnityEvent<float> addPointEvent;
    public UnityEvent pointCollectEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            addPointEvent?.Invoke(points); //Evento para ScoreManager
            pointCollectEvent.Invoke(); //Evento para Sonido
            Destroy(gameObject); //Destruimos
        }
    }
}
