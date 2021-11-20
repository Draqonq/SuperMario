using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;
    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //t_LevelManager.AddLife(gameObject.transform.position + Vector3.up * 2);
            Destroy(gameObject);
        }
    }
}
