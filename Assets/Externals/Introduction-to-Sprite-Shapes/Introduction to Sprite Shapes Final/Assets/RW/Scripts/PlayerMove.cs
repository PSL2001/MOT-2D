/*
 * Copyright (c) 2019 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement Options")]
    public float speed;
    public float jumpForce;

    [Header("References and Layer Masks")]
    public Animator animator;
    //A layer mask that does not include the player layer, so that the ray does not detect the player
    public LayerMask discludePlayerMask;
    public SoundEffects soundEffects;

    [Header("Grounding and Jumping Values")]
    //Distance from the origin of the sprite to the ground, and when it recognises the player as grounded
    public float groundDetectionDistance;
    public float jumpDetectionDistance;
    public float circleHitRadius;
    public float enemyDetectionDistance;

    private bool jumping;
    private bool highSlopeAngle;
    private Rigidbody2D rigidbodyAttached;
    private Vector3 startPosition;
    private Vector3 startRotation;
    private bool gameStarted = false;
    private float notGroundedTimer = 0f;

    void Start() 
    {
        gameStarted = true;
        rigidbodyAttached = GetComponent<Rigidbody2D>();
        //Stores the start position and rotation
        startPosition = transform.position; 
        startRotation = transform.eulerAngles;
    }

    public void Death() 
    {
        //Find GameManager in scene and reset the timer
        if (GameObject.FindObjectOfType<GameManager>() == null) 
        {
            Debug.LogError("GameManager is missing");
        }
        else 
        {
            GameObject.FindObjectOfType<GameManager>().Death();
        }

        transform.position = startPosition;
        transform.eulerAngles = startRotation;
        animator.SetBool("jumping",false);
        animator.SetBool("walking",false);

        soundEffects.PlayerDeathSound();
    }

    void OnDrawGizmos() 
    {
        if (!gameStarted) 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,circleHitRadius);
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position,transform.position+Vector3.down*enemyDetectionDistance);
        }
    }

    void Update() 
    {
        Ray2D r2D = new Ray2D(transform.position,Vector2.down);
        RaycastHit2D hit2D = new RaycastHit2D();
        
        //Returns the horizontal input between -1 and 1 (Smoothed)
        float h = Input.GetAxis("Horizontal");

     
        hit2D = Physics2D.Raycast(r2D.origin,r2D.direction,Mathf.Infinity,discludePlayerMask);

        //Initial value of the right direction
        Vector3 rightDirection = transform.right;
        
        //Check if there is no ground
        if (hit2D.collider == null) 
        {
            notGroundedTimer += Time.deltaTime;

            //If the player has no ground beneath him for more than 1 second then the player is dead
            if (notGroundedTimer > 1f) 
            {
                Death();
            }

        }

        if (hit2D.collider != null && hit2D.distance <= groundDetectionDistance) 
        {
            //Reset the not Grounded Timer
            notGroundedTimer = 0f;

            //Angle of the slope
            float ang = Vector3.Angle(hit2D.normal,Vector3.up);

            if (ang < 45) 
            {
                //Change the upwards direction of the player to the normal of the surface, tilting it towards the normal
                transform.up = Vector3.Lerp(transform.up,hit2D.normal,0.4f);
                highSlopeAngle = false;
            }
            else  
            {
                transform.up = Vector3.Lerp(transform.up,Vector3.up,0.4f);
                highSlopeAngle = true;
            }
            
            //The rightDirection changes based on the slope of the ground
            rightDirection = Vector3.Cross(hit2D.normal,transform.forward).normalized;
            
            //If the player is on the ground, he isn't jumping
            jumping = false;

        }
        else 
        {
            //If we are not on ground, then we rotate the player upwards again
            transform.up = Vector3.Lerp(transform.up,Vector3.up,0.4f);
            animator.SetBool("jumping",true);
        }
        
        //Use a circle cast to check if the player is standing on the enemy for more range
        Ray2D circleRay2D = new Ray2D(transform.position,Vector2.down);
        RaycastHit2D circleHit2D = new RaycastHit2D();

        circleHit2D = Physics2D.CircleCast(circleRay2D.origin,circleHitRadius,circleRay2D.direction,10,discludePlayerMask);
        //If you are standing on an enemy, kill the enemy
        if (circleHit2D.collider != null && circleHit2D.distance <= enemyDetectionDistance)
        {
            if (circleHit2D.transform.CompareTag("Enemy")) 
            {
                circleHit2D.transform.GetComponent<EnemyController>().StartCoroutine("Death");
            }
        }
        
        //Check if the player is high enough, then triggers the jumping animation
        if (hit2D.collider != null) 
        {
            if (hit2D.distance > jumpDetectionDistance && !highSlopeAngle) 
            {
                animator.SetBool("jumping",true);
            }
            else 
            {
                animator.SetBool("jumping",false);
            }

            //Checks if there is or isn't horizontal input, and triggers an animation based on that
            if (Input.GetAxisRaw("Horizontal")==0) 
            {
                animator.SetBool("walking",false);
            }
            else if (Input.GetAxisRaw("Horizontal")!=0 && !jumping && hit2D.distance < groundDetectionDistance)
            {
                animator.SetBool("walking",true);
            }
        }
        
        //Moves the player
        transform.position += rightDirection*h*speed;

        //Checks for the Space key and triggers a jump, only if the player is on the ground
        if ((Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow)) && hit2D.distance <= groundDetectionDistance) 
        {
            //Adds an impulse force to the player upwards
            rigidbodyAttached.AddForce(Vector3.up*jumpForce,ForceMode2D.Impulse);
            transform.up = Vector3.up;
            jumping = true;    
        }

    }

    void OnCollisionEnter2D(Collision2D col) 
    {   
        //If colliding with the flag object
        if (col.transform.CompareTag("Flag")) 
        {
            //Find GameManager in scene and reset the timer
            if (GameObject.FindObjectOfType<GameManager>() == null) 
            {
                Debug.LogError("GameManager is missing!");
            }
            else 
            {
                GameObject.FindObjectOfType<GameManager>().GameCompleted();
                soundEffects.WinningSound();
                speed = 0f;
            }
        }
    }
   
}
