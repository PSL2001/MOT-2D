using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    //Puntos
    [Header("Sistema de puntos")]
    [SerializeField] private int points;

    [Header("Sistema de Stats")]
    [SerializeField] public PlayerStats stats;

    //Metodos de acceso a puntos
    //public int Points { get => points; set => points = value; }

    private void Awake()
    {
        int numInstance = FindObjectsOfType<Data>().Length;

        if (numInstance != 1)
        {
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void AddPoints(int newPoints)
    {
        if (newPoints <= 0) return;
        points += newPoints;
    }
}
