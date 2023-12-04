using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1.0f;

    Rigidbody2D rigidbody2;

    private void Awake()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = rigidbody2.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = UpRightToIsometric(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        rigidbody2.MovePosition(newPos);
    }

    private Vector2 UpRightToIsometric(float horizontal, float vertical) {
        return new Vector2(vertical + horizontal, (vertical - horizontal) / 2).normalized;
    }
}
