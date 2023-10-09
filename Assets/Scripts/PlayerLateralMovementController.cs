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

    //Referencias
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        //Si se pulsa la tecla de espacio
        if (Input.GetKeyDown("space") && rb.position.y <= 0f)
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
        //TODO: Flip Sprite

        //Aplicamos el salto
        if (jump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jump = false;
        }
        rb.velocity = new Vector2(inputX * velocityX, rb.velocity.y);
    }
}
