using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Mushroom, Flower, Tortoise
}

public class Enemy : MonoBehaviour
{
    Vector3 enemyMove;
    bool isDead = false;
    float destroyTimer;
    Animator enemyAnimator;
    BoxCollider2D enemyCollider;
    public EnemyType enemyType;

    //Flower start position
    float flowerStartPositionY;
    bool flowerTransformUp;

    void Start()
    {
        if (enemyType.Equals(EnemyType.Mushroom))
        {
            enemyMove = new Vector3(.0025f, 0);
            enemyAnimator = GetComponent<Animator>();
            enemyAnimator.SetInteger("EnemyType", 0);
            enemyCollider = GetComponent<BoxCollider2D>();
            destroyTimer = 0;
        }
        else if (enemyType.Equals(EnemyType.Flower))
        {
            flowerStartPositionY = transform.position.y;
            flowerTransformUp = true;
            enemyAnimator.SetInteger("EnemyType", 1);
        }
    }

    private void Update()
    {
        if (!isDead && enemyType.Equals(EnemyType.Mushroom))
        {
            transform.position += enemyMove;
        }
        else if(enemyType.Equals(EnemyType.Mushroom))
        {
            destroyTimer += Time.deltaTime;
            if(destroyTimer > 1.2f)
            {
                Destroy(gameObject);
            }
        }
        else if(enemyType.Equals(EnemyType.Flower))
        {
            if (flowerTransformUp) //Up
            {
                transform.position += new Vector3(0, Time.deltaTime * 0.5f, 0);
                if (transform.position.y > flowerStartPositionY + 2)
                {
                    flowerTransformUp = false;
                }
            }
            else //Down
            {
                transform.position -= new Vector3(0, Time.deltaTime * 0.5f, 0);
                if (transform.position.y < flowerStartPositionY)
                {
                    transform.position = new Vector3(transform.position.x, flowerStartPositionY, transform.position.z);
                    flowerTransformUp = true;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Collisions") || collision.gameObject.CompareTag("Enemy")) && enemyType.Equals(EnemyType.Mushroom))
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
        if (!isDead && enemyType.Equals(EnemyType.Mushroom))
        {
            enemyCollider.enabled = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            transform.position -= new Vector3(0, 0.2f, -1);
            isDead = true;
            enemyAnimator.SetBool("isDead", true);
            //enemyCollider.size = new Vector2(enemyCollider.size.x, 0.01f);
            //enemyCollider.offset = new Vector2(enemyCollider.offset.x, -0.25f);
        }
        
    }
}
