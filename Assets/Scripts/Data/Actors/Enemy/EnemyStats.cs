using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStats : Stats
{
    public Type enemyType;

    internal void Update(EnemyStats newStats)
    {
        HP.Update(newStats.HP);
        damage = newStats.damage;
        movementSpeed = newStats.movementSpeed;
        enemyType = newStats.enemyType;
    }
}
