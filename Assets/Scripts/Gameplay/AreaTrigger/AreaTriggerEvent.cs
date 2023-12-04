using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaTriggerEvent : MonoBehaviour
{
    public string trigger = "Player";

    public UnityEvent OnPlayerTriggerEnter;
    public UnityEvent OnPlayerTriggerExit;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(trigger)) OnPlayerTriggerEnter.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(trigger)) OnPlayerTriggerExit.Invoke();

    }
}
