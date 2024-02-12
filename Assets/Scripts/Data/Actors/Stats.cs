using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Stats
{
    [Header("Basic")]
    public Indicator HP;
    public float damage;

    [Header("Movimiento")]
    public float movementSpeed;

    [Header("Attack")]
    //DEPRECATED
    //public GameObject action1Prefab;
    //public GameObject action2Prefab;
    public ActionSO action1;
    public ActionSO action2;

    //Metodos
    public void ResetStats()
    {
        HP.RestartStats();
    }

}
