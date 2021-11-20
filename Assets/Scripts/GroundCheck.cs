using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = transform.parent.GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Foreground" || collision.gameObject.CompareTag("Bricks"))
        {
            playerMovement.isGround = true;
            playerMovement.playerAnimator.SetBool("Jump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Foreground" || collision.gameObject.CompareTag("Bricks"))
        {
            playerMovement.isGround = false;
            playerMovement.playerAnimator.SetBool("Jump", true);
        }
    }
}
