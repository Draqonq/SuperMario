using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public Animator playerAnimator;
    float horizontalMove = 0f;
    BoxCollider2D groundCheck;
    public bool isGround;
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        groundCheck = transform.GetChild(0).GetComponent<BoxCollider2D>();
        isGround = false;
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontalMove, 0, 0) * Time.deltaTime * 3;
        playerAnimator.SetFloat("Horizontal", horizontalMove);

        if (Input.GetKey(KeyCode.W) && Mathf.Abs(rigidBody.velocity.y) < 0.001f && isGround == true)
        {
            rigidBody.AddForce(new Vector2(0, 9f), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
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
    }
}
