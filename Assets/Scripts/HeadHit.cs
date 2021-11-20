using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class HeadHit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bricks"))
        {
            collision.gameObject.GetComponent<BreakBlocks>().BreakBlock();
        }
    }
}
