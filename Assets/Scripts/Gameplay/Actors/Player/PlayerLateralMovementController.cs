using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerLateralMovementController : MonoBehaviour, ActorController
{
    //Referencia a Stats
    [SerializeField] PlayerStats stats;

    //Referencia a PlayerInput
    PlayerInput playerInput;
    InputAction m_movementAction, m_jumpAction;

    //Parametros privados para gestionar el input
    float inputX;
    //Para guardar la aceleracion actual
    [SerializeField] float currentSpeed;
    //[SerializeField] permite ver los campos en Unity
    private bool jump = false;
    Vector2 inputY;

    //Informacion para salto
    private bool isGrounded = false;
    private int numberOfJumps;

    //Eventos
    public UnityEvent onDie = new();

    //Referencias
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        //Referencia a componentes Externos
        stats = GameObject.FindGameObjectWithTag("GameData").GetComponent<Data>().stats;
        numberOfJumps = stats.numberOfJumps;
        

        playerInput = GameObject.FindGameObjectWithTag("PlayerInput").GetComponent<PlayerInput>();
        m_movementAction = playerInput.actions["Move"];
        m_jumpAction = playerInput.actions["Jump"];
        

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        currentSpeed = 0;

        //Me suscribo a los cambios de HP de los stats
        stats.HP.RestartStats();
        stats.HP.OnIndicatorChange.AddListener(onHPUpdate);
    }

    private void onHPUpdate(float val)
    {
        if (val <= 0)
        {
            onDie.Invoke();
            
        }
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
       
        
        //TODO: Flip Sprite
        Vector2 aux = spriteRenderer.transform.localScale;
        if (rb.velocity.x > 0) aux.x = Mathf.Abs(aux.x);
        else if (rb.velocity.x < 0) aux.x = -Mathf.Abs(aux.x);
        spriteRenderer.transform.localScale = aux;
    }

    private void Jump()
    {
        //Aplicamos el salto
    
        if (jump)
        {
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

    public Stats GetStats()
    {
        return stats;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void OnHealing(float healAmmount)
    {
        throw new System.NotImplementedException();
    }

    public void OnDamage(float damageAmmount)
    {
        stats.HP.CurrentValue -= damageAmmount;
    }
}
