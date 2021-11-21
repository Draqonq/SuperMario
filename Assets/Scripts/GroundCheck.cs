using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    Mario marioMovement;

    private void Start()
    {
        marioMovement = transform.parent.GetComponent<Mario>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
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
            Debug.Log("Smierc");
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
