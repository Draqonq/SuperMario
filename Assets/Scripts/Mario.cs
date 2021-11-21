using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    Rigidbody2D rigidBody;
    public Animator playerAnimator;
    float horizontalMove = 0f;
    BoxCollider2D groundCheck;
    public bool isGround;
    PlayerData playerData;
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        groundCheck = transform.GetChild(0).GetComponent<BoxCollider2D>();
        playerData = GameObject.Find("Data").GetComponent<PlayerData>();
        isGround = false;
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontalMove, 0, 0) * Time.deltaTime * 4;
        playerAnimator.SetFloat("Horizontal", horizontalMove);

        if (Input.GetKey(KeyCode.W) && Mathf.Abs(rigidBody.velocity.y) < 0.001f && isGround == true)
        {
            rigidBody.AddForce(new Vector2(0, 9f), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !collision.gameObject.GetComponent<Enemy>().IsDead())
        {
            playerData.LevelDown();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Teleport"))
        {
            if (collision.gameObject.GetComponent<Teleport>().GetIsKey())
            {
                if (Input.GetKey(KeyCode.S))
                {
                    transform.position = collision.gameObject.GetComponent<Teleport>().GetTeleportPosition();
                }
            }
            else
            {
                transform.position = collision.gameObject.GetComponent<Teleport>().GetTeleportPosition();
            }
        }
    }

    /*private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Foreground" || collision.gameObject.CompareTag("Bricks"))
        {
            isGround = true;
            playerAnimator.SetBool("Jump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Foreground" || collision.gameObject.CompareTag("Bricks"))
        {
            isGround = false;
            playerAnimator.SetBool("Jump", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bricks"))
        {
            collision.gameObject.GetComponent<BreakBlocks>().BreakBlock();
        }
    }*/
}
