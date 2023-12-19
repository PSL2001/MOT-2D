using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AttackMoveTowards : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector2 dir;

    //Referencia a componentes
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
       rb = GetComponent<Rigidbody2D>();
       rb.velocity = dir * speed;
    }

    public void Initialize(float newSpeed, Vector2 newDir)
    {
        speed = newSpeed;
        dir = newDir;
        rb.velocity = dir * speed;
    }
}
