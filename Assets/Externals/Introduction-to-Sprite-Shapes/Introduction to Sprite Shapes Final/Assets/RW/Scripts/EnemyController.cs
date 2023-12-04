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

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public BoxCollider2D boxCollider;
    public SpriteRenderer spriteRenderer;
    public LayerMask playerMask;

    [Header("Movement Options")]
    public float patrolSpeed;
    public Vector3 rightPoint;
    public Vector3 leftPoint;

    private bool gameStarted;
    private Vector3 startPoint;
    private bool dead;

    private bool travellingToRightPoint = true;
    private bool completedPath = false;
    private Vector3 currentPoint;

    void Start() 
    {
        gameStarted = true;
        startPoint = transform.position;
        transform.position = startPoint+rightPoint;
    }

    void OnDrawGizmos() 
    {
        if (!gameStarted) 
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position,transform.position+rightPoint);
            Gizmos.DrawLine(transform.position,transform.position+leftPoint);
        }

    }

    private void OnCollisionEnter2D (Collision2D col) 
    {
        if (col.transform.GetComponent<PlayerMove>() != null & dead != true) 
        {
            col.transform.GetComponent<PlayerMove>().Death();
        }
    }


    public IEnumerator Death() 
    {
        animator.SetBool("dead",true);
        dead = true;

        yield return new WaitForSeconds(0.1f);
        
        boxCollider.enabled = false;
        
        yield return new WaitForSeconds(0.4f);

        if (FindObjectOfType<SoundEffects>() != null)  
        {
            GameObject.FindObjectOfType<SoundEffects>().EnemyDeathSound();
        }

        GameObject.Destroy(this.gameObject);
    }

    void Update() 
    {

        if (travellingToRightPoint)
        {
            completedPath = (Vector2.Distance(transform.position,startPoint+rightPoint)<0.1f);
            currentPoint = rightPoint;
            spriteRenderer.flipX = true;
        }
        else 
        {
            completedPath = (Vector2.Distance(transform.position,startPoint+leftPoint)<0.1f);
            currentPoint = leftPoint;
            spriteRenderer.flipX = false;
        }
        
        if (!completedPath) 
        {
            transform.position += ((startPoint+currentPoint)-transform.position).normalized*patrolSpeed;
        }
        else 
        {
            travellingToRightPoint = !travellingToRightPoint;
            completedPath = false;
        }

    }

}

