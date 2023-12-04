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
    public UnityEvent pointCollectEvent;
    
    //Referencia a componente interno
    Data data;
    void Awake()
    {
        if (GameObject.FindGameObjectWithTag("GameData"))
        {
            data = GameObject.FindGameObjectWithTag("GameData").GetComponent<Data>();
        }
        else
        {
            Debug.Log("Error! No se encuentra el objeto con el tag de GameData!!!!!!!!");
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            data?.AddPoints(points); // Sumo los puntos a data
            pointCollectEvent.Invoke(); //Llamo al evento
            Destroy(gameObject); //Destruimos
        }
    }
}
