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
        enemyAnimator = GetComponent<Animator>();
        enemyCollider = GetComponent<BoxCollider2D>();
        if (enemyType.Equals(EnemyType.Mushroom))
        {
            enemyMove = new Vector3(.04f, 0);
            enemyAnimator.SetInteger("EnemyType", 0);
            destroyTimer = 0;
        }
        else if (enemyType.Equals(EnemyType.Tortoise))
        {
            enemyMove = new Vector3(.04f, 0);
            enemyAnimator.SetInteger("EnemyType", 2);
            enemyAnimator.SetFloat("Horizontal", 1);
            destroyTimer = 0;
        }
        else if (enemyType.Equals(EnemyType.Flower))
        {
            flowerStartPositionY = transform.position.y;
            flowerTransformUp = true;
            enemyAnimator.SetInteger("EnemyType", 1);
        }
    }

    private void FixedUpdate()
    {
        //Grzyb
        if (enemyType.Equals(EnemyType.Mushroom))
        {
            if (!isDead)
            {
                transform.position += enemyMove;
            }
            else
            {
                destroyTimer += Time.fixedDeltaTime;
                if (destroyTimer > 1.2f)
                {
                    Destroy(gameObject);
                }
            }
        }
        //¯ó³w
        else if (enemyType.Equals(EnemyType.Tortoise))
        {
            if (!isDead)
            {
                transform.position += enemyMove;
            }
            else
            {
                destroyTimer += Time.fixedDeltaTime;
                if (destroyTimer > 1.2f)
                {
                    Destroy(gameObject);
                }
            }
        }
        //Roœlinka
        else if(enemyType.Equals(EnemyType.Flower))
        {
            if (flowerTransformUp) //Up
            {
                transform.position += new Vector3(0, Time.fixedDeltaTime * 0.5f, 0);
                if (transform.position.y > flowerStartPositionY + 2)
                {
                    flowerTransformUp = false;
                }
            }
            else //Down
            {
                transform.position -= new Vector3(0, Time.fixedDeltaTime * 0.5f, 0);
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
        if ((collision.gameObject.CompareTag("Collisions") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boost")) 
            && (enemyType.Equals(EnemyType.Mushroom) || enemyType.Equals(EnemyType.Tortoise)))
        {
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                enemyMove = new Vector3(-.04f, 0);
                enemyAnimator.SetFloat("Horizontal", -1);
            }
            else
            {
                enemyMove = new Vector3(.04f, 0);
                enemyAnimator.SetFloat("Horizontal", 1);
            }
        }
        else if (collision.gameObject.CompareTag("FieldOfDeath"))
        {
            Destroy(gameObject);
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void Dead()
    {
        if (!isDead 
            && (enemyType.Equals(EnemyType.Mushroom) || enemyType.Equals(EnemyType.Tortoise)))
        {
            enemyCollider.enabled = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            transform.position -= new Vector3(0, 0.2f, -1);
            isDead = true;
            enemyAnimator.SetBool("isDead", true);
            //enemyCollider.size = new Vector2(enemyCollider.size.x, 0.01f);
            //enemyCollider.offset = new Vector2(enemyCollider.offset.x, -0.25f);
        }
        else if (!isDead && enemyType.Equals(EnemyType.Flower))
        {
            Destroy(gameObject);
        }


    }
}
