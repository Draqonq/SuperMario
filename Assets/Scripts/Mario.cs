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
    //Win Level
    bool isWinLevel;

    //Button movement
    bool buttonLeft;
    bool buttonRight;
    bool buttonUp;
    bool buttonDown;

    //Fireball
    public Fireball fireball;

    //Sound
    public SoundManager soundManager;

    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        groundCheck = transform.GetChild(0).GetComponent<BoxCollider2D>();
        playerData = GameObject.Find("Data").GetComponent<PlayerData>();
        isGround = false;
        isAlive = true;
        isWinLevel = false;
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            //Keyboard movement
            if(!buttonRight && !buttonLeft)
            {
                horizontalMove = Input.GetAxis("Horizontal");
                transform.position += new Vector3(horizontalMove, 0, 0) * Time.fixedDeltaTime * 4;
            }

            //Button movement
            if (buttonRight)
            {
                horizontalMove = 1;
                transform.position += new Vector3(horizontalMove, 0, 0) * Time.fixedDeltaTime * 4;
                //playerAnimator.SetFloat("Horizontal", horizontalMove);
            }
            if (buttonLeft)
            {
                horizontalMove = -1;
                transform.position += new Vector3(horizontalMove, 0, 0) * Time.fixedDeltaTime * 4;
                //playerAnimator.SetFloat("Horizontal", horizontalMove);
            }

            //Animation
            playerAnimator.SetFloat("Horizontal", horizontalMove);

            //Jump
            if ((buttonUp || Input.GetKey(KeyCode.W)) && Mathf.Abs(rigidBody.velocity.y) < 0.001f && isGround == true)
            {
                rigidBody.AddForce(new Vector2(0, 9f), ForceMode2D.Impulse);
                soundManager.PlaySound(SoundName.Jump);
            }

            //Shoot
            if (Input.GetKey(KeyCode.Space) && playerData.GetLevel() > 2)
            {
                fireball.Shoot(horizontalMove);
            }

        }
        if (isWinLevel && transform.position.y > -1.5f)
        {
            //WIN
            transform.position -= new Vector3(0, Time.fixedDeltaTime * 2.5f, 0);
        }
        else if(isWinLevel && transform.position.y <= -1.5f)
        {
            playerData.winLevel = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !collision.gameObject.GetComponent<Enemy>().IsDead())
        {
            playerData.LevelDown();
        }
        else if (collision.gameObject.CompareTag("End"))
        {
            EndLevel(collision.gameObject.GetComponent<EndRope>().ropeXPosition);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Teleport"))
        {
            if (collision.gameObject.GetComponent<Teleport>().GetIsKey())
            {
                if ((Input.GetKey(KeyCode.S) || buttonDown) && isGround)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerData.Death();
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

    public void ButtonShoot()
    {
        if(playerData.GetLevel() > 2)
        {
            fireball.Shoot(horizontalMove);
        }
    }

    public void EndLevel(float endXPosition)
    {
        GetComponent<AudioSource>().Stop();
        soundManager.PlaySound(SoundName.WinGame);
        transform.position = new Vector3(endXPosition, transform.position.y, 0);
        playerAnimator.SetBool("WinLevel", true);
        isAlive = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
        isWinLevel = true;
    }
}
