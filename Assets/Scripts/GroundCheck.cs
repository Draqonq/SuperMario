using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    Mario marioMovement;
    PlayerData playerData;

    private void Start()
    {
        marioMovement = transform.parent.GetComponent<Mario>();
        playerData = GameObject.Find("Data").GetComponent<PlayerData>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerData.AddScore(100);
            collision.gameObject.GetComponent<Enemy>().Dead();
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Foreground" 
            || collision.gameObject.CompareTag("Bricks") 
            || collision.gameObject.CompareTag("Enemy") 
            || collision.gameObject.CompareTag("Collisions")
            || collision.gameObject.CompareTag("Teleport"))
        {
            marioMovement.isGround = true;
            marioMovement.playerAnimator.SetBool("Jump", false);
        }
        else if (collision.gameObject.CompareTag("FieldOfDeath"))
        {
            playerData.Death();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Foreground" 
            || collision.gameObject.CompareTag("Bricks") 
            || collision.gameObject.CompareTag("Enemy")
            || collision.gameObject.CompareTag("Collisions")
            || collision.gameObject.CompareTag("Teleport"))
        {
            marioMovement.isGround = false;
            marioMovement.playerAnimator.SetBool("Jump", true);
        }
    }
}
