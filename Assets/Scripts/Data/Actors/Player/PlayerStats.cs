using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats : Stats
{
    [Header("Movimiento")]
    public float acceleration;
    [Range(0, 1)] public float airMomentum;

    [Header("Salto")]
    public float jumpSpeed;
    public int numberOfJumps;

    [Header("Invulnerabilidad")]
    public Color invulnerabilityColor = Color.red;
    [Range(0, 3)] public float invulnerabilitySeconds = 1f;

}
