using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    [SerializeField]
    public float runVelocity = 20f;
    [SerializeField]
    public float jumpVelocity = 100f; // I made these values up,
    [SerializeField]
    public float fallVelocity = 10f; // they will need to be tweaked

    private CapsuleCollider2D playerCollider;
    private Rigidbody2D rb;
    [SerializeField]
    private LayerMask platformLayerMask;
    private Vector2 startPosition;


    private void Awake()
    {
        startPosition = new Vector2(transform.position.x, transform.position.y);
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void Update()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0) * runVelocity * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0) * runVelocity * Time.deltaTime;
        }
        // jumping/falling
        if (isGrounded())
        {
            if (Input.GetKey(KeyCode.Space))
            {
                // if you hit jump then give it some vertical speed
                rb.velocity = Vector2.up * jumpVelocity;
                //transform.position += new Vector3(0, 1) * jumpSpeed * Time.deltaTime;
            }
        }
        else
        {
            // if you're not touching the ground, reduce your vertical
            // speed a little bit every frame.
            rb.velocity = Vector2.down * fallVelocity;
        }

        if (Input.GetKeyDown(KeyCode.R)) resetCharacter();
    }

    private bool isGrounded()
    {
        float extraHeightTest = 0.1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y + extraHeightTest, platformLayerMask);
        Color rayColor;

        if (raycastHit.collider != null)
        { //if we hit something
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(playerCollider.bounds.center, Vector2.down * (playerCollider.bounds.extents.y + extraHeightTest), rayColor);
        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    private void resetCharacter()
    {
        transform.position = startPosition;
        rb.velocity = Vector2.zero;
    }
    private void onFire()
    {

    }
}
