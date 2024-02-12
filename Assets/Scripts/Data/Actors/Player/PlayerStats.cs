using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public bool invulnerable = false;
    public Color invulnerabilityColor = Color.red;
    [Range(0, 3)] public float invulnerabilitySeconds = 1f;

    public void Update(PlayerStats newStats)
    {
        HP.Update(newStats.HP);
        damage = newStats.damage;
        movementSpeed = newStats.movementSpeed;
        acceleration = newStats.acceleration;

        action1 = newStats.action1;
        action2 = newStats.action2;

        airMomentum = newStats.airMomentum;
        jumpSpeed = newStats.jumpSpeed;
        numberOfJumps = newStats.numberOfJumps;
        invulnerabilityColor = newStats.invulnerabilityColor;
        invulnerabilitySeconds = newStats.invulnerabilitySeconds;
    }

}
