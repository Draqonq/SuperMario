using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public Vector2 followOffset;
    private Vector2 threshold;
    public float speed = 4;
    private Rigidbody2D rb;
    public GameObject borderLeft;
    public GameObject borderTop;
    public float cam;

    void Start()
    {
        threshold = calculateThreshold();
        rb = target.GetComponent<Rigidbody2D>();
        cam = Camera.main.orthographicSize * Camera.main.aspect;
        float camSize = (-cam - (0.09f * Camera.main.orthographicSize));
        borderLeft.transform.localPosition = new Vector3(camSize, 0, 1);
        borderTop.transform.localScale = new Vector3(-camSize * 2, borderTop.transform.localScale.y, borderTop.transform.localScale.z);
        
    }
    void FixedUpdate()
    {
        Vector2 follow = target.transform.position;
        float xDiference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        //float yDiference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = transform.position;

        if (Mathf.Abs(xDiference) >= threshold.x && follow.x > transform.position.x) {
            newPosition.x = follow.x;
        }

        /*if (Mathf.Abs(yDiference) >= threshold.y)
        {
            newPosition.y = follow.y;
        }*/

        //camera speed equal to mario movement speed if he is going faster, or deafault if he doesnt
        float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.fixedDeltaTime);
    }

    private Vector3 calculateThreshold() {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = calculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }

    public void TeleportPosition(float yTeleportPosition)
    {
        transform.position = new Vector3(target.transform.position.x, yTeleportPosition, transform.position.z);
    }

}
