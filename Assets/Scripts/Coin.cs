using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public PlayerData playerData;
    public Vector3 dropCoinSpeed = new Vector3(0, 0.01f);
    Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        DropCoin();
    }

    void DropCoin()
    {
        if (transform.position.y < (startPosition.y + 1))
        {
            transform.position += dropCoinSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log("Coin+");
            playerData.AddCoins(1);
        }
    }

}
