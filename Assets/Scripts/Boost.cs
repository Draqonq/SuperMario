using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    PlayerData playerData;
    
    public Vector3 dropBoostSpeed = new Vector3(0, 0.01f);
    Vector3 startPosition;

    bool isPushed;
    Vector3 boostMove;
    bool isCollected = false;
    void Start()
    {
        startPosition = transform.position;
        playerData = GameObject.Find("Data").GetComponent<PlayerData>();
    }

    private void Update()
    {
        DropBoost();
        MoveBoost();
    }

    void DropBoost()
    {
        if (transform.position.y < (startPosition.y + .5f) && !isPushed)
        {
            transform.position += dropBoostSpeed;
        }
        else if (!isPushed)
        {
            isPushed = true;
            boostMove = new Vector3(.0025f, 0);
        }
    }

    void MoveBoost()
    {
        if (isPushed)
        {
            transform.position += boostMove;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCollected && collision.gameObject.CompareTag("Player"))
        {
            isCollected = true;
            playerData.LevelUp();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Collisions"))
        {
            if(collision.gameObject.transform.position.x > transform.position.x)
            {
                boostMove = new Vector3(-0.0025f, 0);
            }
            else
            {
                boostMove = new Vector3(0.0025f, 0);
            }
        }
    }
}
