using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerData", menuName ="Actor/PlayerData", order = 1)]
public class PlayerDataSO : ScriptableObject
{
    [Header("Info")]
    public Sprite sprite;
    public AnimatorController animator;
    public Color color = Color.white;

    //[Header("Stats")]
    //public PlayerStats playerStats;

    public void Initialize(PlayerController player)
    {
        GameObject gameObject = player.GetGameObject();
        SpriteRenderer spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        Animator ani = gameObject.GetComponentInChildren<Animator>();

        spriteRenderer.sprite = sprite;
        spriteRenderer.color = color;
        ani.runtimeAnimatorController = animator;

        //Inicializo los stats
        //PlayerStats stats = (PlayerStats) player.GetStats();
        //stats.Update(playerStats);

        //Actualizo el collider

    }
}
