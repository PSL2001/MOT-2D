using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemiesController))]
public class EnemyPatrol : MonoBehaviour
{
    //Referencia al enemycontroller
    EnemyStats stats;

    //Patrulla
    [SerializeField] int currentDestination = 0;
    [SerializeField] List<Vector3> patrolDestination;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;

    [SerializeField]bool invertedFlip = false;

    void Start()
    {
        stats = (EnemyStats) GetComponent<EnemiesController>().GetStats();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        //Obtengo el punto donde comienza
        patrolDestination.Add(transform.position);

        //Rellenar con la ruta
        Transform patrolGD = gameObject.transform.Find("Patrol");

        for (int i = 0; i < patrolGD.childCount; i++)
        {
            patrolDestination.Add(patrolGD.GetChild(i).position);
        }

        //inicializo la posicion de destino
        currentDestination = 0;

        StartCoroutine("Patrol");
    }

    private void Update()
    {
        //Patrol();
        //Miro si hace falta flip
        //Flip();
        //Paso la informacion al animator
        UpdateAnimatorParameters();
    }

    private void UpdateAnimatorParameters()
    {
        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocityY", Mathf.Abs(rb.velocity.y));
        animator.SetBool("isGrounded", Utils.Utils.IsGrounded2D(gameObject));
    }

     private IEnumerator Patrol()
    {
        while (true)
        {
            //Compruebo si he llegado al destino actual y si es el caso lo cambio
            if (Vector3.Distance(transform.position, patrolDestination[currentDestination]) < 0.3f)
            {
                currentDestination = (currentDestination + 1) % patrolDestination.Count;
                yield return new WaitForSecondsRealtime(1.0f);
            }
            // Me muevo a mi destino
            Vector3 dir = (patrolDestination[currentDestination] - transform.position).normalized;

            rb.velocity = dir * stats.movementSpeed;

            Flip();

            yield return new WaitForEndOfFrame();
        }
    }

    private void Flip()
    {
        Vector2 aux = spriteRenderer.transform.localScale;
        if (rb.velocity.x > 0) aux.x = Mathf.Abs(aux.x);
        else if (rb.velocity.x < 0) aux.x = -Mathf.Abs(aux.x);

        if (invertedFlip) aux.x *= -1;


        spriteRenderer.transform.localScale = aux;

    }
}
