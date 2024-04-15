using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AddTime : MonoBehaviour
{
    //Datos propios
    [SerializeField][Range(1, 10)] private float time = 1;

    //Eventos
    public UnityEvent<float> addTimeEvent;
    public UnityEvent timeCollectEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            addTimeEvent?.Invoke(time); //Evento para ScoreManager
            timeCollectEvent.Invoke(); //Evento para Sonido
            Destroy(gameObject); //Destruimos
        }
    }
}
