using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPref : MonoBehaviour
{
    [SerializeField] string id = "key";

    private void Awake()
    {
        if (PlayerPrefs.GetInt(id, 0) == 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            PlayerPrefs.SetInt(id, 1);
            //....
            Destroy(gameObject);
        }
    }
}
