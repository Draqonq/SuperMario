using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerController;
    public Animator playerAnimator;
    float horizontalMove = 0f;
    BoxCollider2D groundCheck;
    public bool isGround;
    void Start()
    {
        playerController = gameObject.GetComponent<Rigidbody2D>();
        groundCheck = transform.GetChild(0).GetComponent<BoxCollider2D>();
        isGround = false;
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontalMove, 0, 0) * Time.deltaTime * 3;
        playerAnimator.SetFloat("Horizontal", horizontalMove);

        if (Input.GetKey(KeyCode.W) && Mathf.Abs(playerController.velocity.y) < 0.001f && isGround == true)
        {
            playerController.AddForce(new Vector2(0, 9f), ForceMode2D.Impulse);
        }
    }
}
