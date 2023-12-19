using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerLateralMovementController : MonoBehaviour
{
    //Referencia a Stats
    //Referencia a PlayerController
    PlayerController controller;
    PlayerStats stats;

    //Parametros privados para gestionar el input
    float inputX;
    //Para guardar la aceleracion actual
    [SerializeField] float currentSpeed;
    //[SerializeField] permite ver los campos en Unity
    private bool jump = false;
    Vector2 inputY;

    InputAction m_movementAction, m_jumpAction;

    public UnityEvent onJump;

    //Informacion para salto
    private bool isGrounded = false;
    private int numberOfJumps;

    //Referencias
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {

        controller = GetComponent<PlayerController>();

        //Referencia a componentes Externos
        stats = (PlayerStats) controller.GetStats();
        numberOfJumps = stats.numberOfJumps;

        m_movementAction = controller.playerInput.actions["Move"];
        m_jumpAction = controller.playerInput.actions["Jump"];


        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        currentSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {

        inputX = m_movementAction.ReadValue<Vector2>().x;
        
        //Si se pulsa la tecla de espacio
        if (m_jumpAction.triggered && numberOfJumps != 0)
        {
            jump = true;
        //    Debug.Log("Estoy pulsando espacio");
        //    //Queremos saltar
        //    inputY = new Vector2(0, 1.0f * jumpForce);
        //    //rb.velocity = inputY;
        //    rb.AddForce(inputY, ForceMode2D.Force);
        }
        if (isGrounded)
        {
            numberOfJumps = stats.numberOfJumps;
        }

    }

    private void FixedUpdate()
    {
        LateralMove();
        Jump();

        //Actualizamos el animator
        UpdateAnimatorParameters();
    }

    private void LateralMove()
    {
        if (inputX != 0)
        {
            currentSpeed += stats.acceleration * Time.deltaTime;
            if (currentSpeed >= stats.movementSpeed) { currentSpeed = stats.movementSpeed; }
            if (isGrounded)
            {
                rb.velocity = new Vector2(inputX * currentSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(inputX * (currentSpeed * stats.airMomentum), rb.velocity.y);
            }
        } else
        {
            currentSpeed -= stats.acceleration * Time.deltaTime;
            if (currentSpeed <= 0) { currentSpeed = 0; }
            if (isGrounded)
            {
                rb.velocity = new Vector2(inputX * currentSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(inputX * (currentSpeed * stats.airMomentum), rb.velocity.y);
            }
        }
       
        
        Flip();
    }

    private void Flip()
    {
        //TODO: Flip Sprite con escala
        //Vector2 aux = spriteRenderer.transform.localScale;
        //if (rb.velocity.x > 0) aux.x = Mathf.Abs(aux.x);
        //else if (rb.velocity.x < 0) aux.x = -Mathf.Abs(aux.x);
        //spriteRenderer.transform.localScale = aux;

        //Flip usando rotacion
        if (rb.velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (rb.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void Jump()
    {
        //Aplicamos el salto
        if (jump)
        {
            onJump.Invoke();
            numberOfJumps--;
            rb.AddForce(Vector2.up * stats.jumpSpeed, ForceMode2D.Impulse);
            jump = false;

        } else
        {
            jump = false;
        }

    }

    private void UpdateAnimatorParameters()
    {
        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocityY", Mathf.Abs(rb.velocity.y));
        animator.SetBool("isGrounded", isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }

    public bool IsGrounded()
    {
        //var hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f);
        //if (!hit) return false;
        return Physics2D.Raycast(transform.position, Vector2.down, 0.5f);

    }
}
