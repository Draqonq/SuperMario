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
    public bool isAlive;
    PlayerData playerData;
    public CameraFollow cameraFollow;

    //Button movement
    bool buttonLeft;
    bool buttonRight;
    bool buttonUp;
    bool buttonDown;

    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        groundCheck = transform.GetChild(0).GetComponent<BoxCollider2D>();
        playerData = GameObject.Find("Data").GetComponent<PlayerData>();
        isGround = false;
        isAlive = true;
    }

    void Update()
    {
        if (isAlive)
        {
            //Keyboard movement
            if(!buttonRight && !buttonLeft)
            {
                horizontalMove = Input.GetAxis("Horizontal");
                transform.position += new Vector3(horizontalMove, 0, 0) * Time.deltaTime * 4;
            }

            //Button movement
            if (buttonRight)
            {
                horizontalMove = 1;
                transform.position += new Vector3(horizontalMove, 0, 0) * Time.deltaTime * 4;
                //playerAnimator.SetFloat("Horizontal", horizontalMove);
            }
            if (buttonLeft)
            {
                horizontalMove = -1;
                transform.position += new Vector3(horizontalMove, 0, 0) * Time.deltaTime * 4;
                //playerAnimator.SetFloat("Horizontal", horizontalMove);
            }

            //Animation
            playerAnimator.SetFloat("Horizontal", horizontalMove);

            //Jump
            if ((buttonUp || Input.GetKey(KeyCode.W)) && Mathf.Abs(rigidBody.velocity.y) < 0.001f && isGround == true)
            {
                rigidBody.AddForce(new Vector2(0, 9f), ForceMode2D.Impulse);
            }
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
                if (Input.GetKey(KeyCode.S) || buttonDown)
                {
                    transform.position = collision.gameObject.GetComponent<Teleport>().GetTeleportPosition();
                    cameraFollow.TeleportPosition(-17);
                }
            }
            else
            {
                transform.position = collision.gameObject.GetComponent<Teleport>().GetTeleportPosition();
                cameraFollow.TeleportPosition(1);
            }
        }
    }

    public void ButtonRightOn()
    {
        if (!buttonLeft)
        {
            buttonRight = true;
        }
    }

    public void ButtonRightOff()
    {
        if(!buttonLeft && buttonRight)
        {
            buttonRight = false;
        }
    }

    public void ButtonLeftOn()
    {
        if (!buttonRight)
        {
            buttonLeft = true;
        }
    }

    public void ButtonLeftOff()
    {
        if (!buttonRight && buttonLeft)
        {
            buttonLeft = false;
        }
    }

    public void ButtonUpOn()
    {
        buttonUp = true;
    }

    public void ButtonUpOff()
    {
        buttonUp = false;
    }

    public void ButtonDownOn()
    {
        buttonDown = true;
    }

    public void ButtonDownOff()
    {
        buttonDown = false;
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
