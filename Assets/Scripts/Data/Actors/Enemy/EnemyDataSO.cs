using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "new EnemyData", menuName = "Actor/EnemyData", order = 1)]
public class EnemyDataSO : ScriptableObject
{
    [Header("Info")]
    public Sprite sprite;
    public AnimatorController animator;
    public Color color = Color.white;

    [Header("Stats")]
    public EnemyStats enemyStats;

    public void Initialize(EnemiesController enemy)
    {
        GameObject gameObject = enemy.GetGameObject();
        SpriteRenderer spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        Animator ani = gameObject.GetComponentInChildren<Animator>();

        spriteRenderer.sprite = sprite;
        spriteRenderer.color = color;
        ani.runtimeAnimatorController = animator;

        //Inicializo los Stats
        EnemyStats stats = (EnemyStats) enemy.GetStats();
        stats.Update(enemyStats);

    }
}
