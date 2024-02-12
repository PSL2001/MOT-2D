using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action", menuName = "Actions/Attacks/Prefab Attack")]
public class AttackPrefabSO : ActionSO
{
    [Header("Basic Attack Info")]
    public float damage;
    public float speed;

    public enum AttackType { Melee, Distance };
    public AttackType type;

    [Header("Prefab Attack Info")]
    public GameObject prefab;

    public override void Use(GameObject origin)
    {
        //Usamos el metodo heredado
        base.Use(origin);

        //Calculo el daño
        float d = damage; //Multiplicar por daño del jugador

        //Instancio el ataque
        GameObject att;
        if (type == AttackType.Melee)
        {
            att = Instantiate(prefab, origin.transform); //Instanciar como hijo
        }
        else
        {
            att = Instantiate(prefab, origin.transform.position, Quaternion.identity); //Instanciar como elemento independiente en el mundo
        }

        att.layer = origin.layer;

        //Inicializo componentes
        att.GetComponent<OnAttackImpact>()?.Initialize(d);
        if (type == AttackType.Melee)
        {
            Animator animator = att.GetComponent<Animator>();
            animator.speed = speed;
            float currentAnimationClipSeconds = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length / speed;
            origin.GetComponent<PlayerController>()?.ApplyTempInvulnerabilityWithotColor2D(currentAnimationClipSeconds);
        }

        if (type == AttackType.Distance)
        {
            att.GetComponent<AttackMoveTowards>()?.Initialize(speed, origin.transform.right);
        }
    }
}
