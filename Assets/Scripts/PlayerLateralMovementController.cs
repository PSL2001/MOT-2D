using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerLateralMovementController : MonoBehaviour
{
    //Parametros Publicos
    public float velocityX = 5;
    public float jumpForce = 5;

    //Parametros privados para gestionar el input
    float inputX;
    //[SerializeField] permite ver los campos en Unity
    private bool jump = false;
    Vector2 inputY;

    //Informacion para salto
    private bool isGrounded = false;

    //Referencias
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        inputX = Input.GetAxis("Horizontal");
        //Si se pulsa la tecla de espacio
        if (Input.GetKeyDown("space") && isGrounded)
        {
            jump = true;
        //    Debug.Log("Estoy pulsando espacio");
        //    //Queremos saltar
        //    inputY = new Vector2(0, 1.0f * jumpForce);
        //    //rb.velocity = inputY;
        //    rb.AddForce(inputY, ForceMode2D.Force);
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(inputX * velocityX, rb.velocity.y);
        //TODO: Flip Sprite
        Vector2 aux = spriteRenderer.transform.localScale;
        if (rb.velocity.x > 0) aux.x = Mathf.Abs(aux.x);
        else if (rb.velocity.x < 0) aux.x = -Mathf.Abs(aux.x);
        spriteRenderer.transform.localScale = aux;
        //Aplicamos el salto
        if (IsGrounded() && jump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jump = false;
        }
        
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
        return Physics2D.Raycast(transform.position, Vector2.down, 2.5f);

    }
}
