using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Vector2 velocity;
    Rigidbody2D rb;
    SpriteRenderer sr;
    CircleCollider2D col;
    public Vector2 startPosition;
    bool isShooting;
    float shootingTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        col = GetComponent<CircleCollider2D>();
        col.enabled = false;
        startPosition = transform.localPosition;
    }

    private void FixedUpdate()
    {
        if (isShooting)
        {
            shootingTimer += Time.fixedDeltaTime;
            if (rb.velocity.y < velocity.y)
            {
                rb.velocity = velocity;
            }
            if(shootingTimer >= 5)
            {
                StopShoot();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = new Vector2(velocity.x, -velocity.y);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            StopShoot();
        }
        else if (collision.gameObject.CompareTag("End"))
        {
            StopShoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Dead();
            StopShoot();
        }
    }

    public void Shoot()
    {
        if (!isShooting)
        {
            transform.localPosition = startPosition;
            rb.bodyType = RigidbodyType2D.Dynamic;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            col.enabled = true;
            rb.velocity = new Vector2(velocity.x, -velocity.y);
            shootingTimer = 0;
            isShooting = true;
        }
    }

    void StopShoot()
    {
        isShooting = false;
        rb.bodyType = RigidbodyType2D.Static;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        col.enabled = false;
    }
}
