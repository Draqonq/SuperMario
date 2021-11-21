using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector3 enemyMove;
    bool isDead = false;
    Animator enemyAnimator;
    BoxCollider2D enemyCollider;

    void Start()
    {
        enemyMove = new Vector3(.0025f, 0);
        enemyAnimator = GetComponent<Animator>();
        enemyCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (!isDead)
        {
            transform.position += enemyMove;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Collisions") || collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                enemyMove = new Vector3(-0.0025f, 0);
            }
            else
            {
                enemyMove = new Vector3(0.0025f, 0);
            }
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void Dead()
    {
        if (!isDead)
        {
            isDead = true;
            enemyAnimator.SetBool("isDead", true);
            enemyCollider.size = new Vector2(enemyCollider.size.x, 0.01f);
            enemyCollider.offset = new Vector2(enemyCollider.offset.x, -0.25f);
        }
        
    }
}
