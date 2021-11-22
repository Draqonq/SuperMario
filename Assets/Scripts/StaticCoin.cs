using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCoin : MonoBehaviour
{
    PlayerData playerData;
    void Start()
    {
        playerData = GameObject.Find("Data").GetComponent<PlayerData>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Coin+");
            playerData.AddCoins(1);
            playerData.AddScore(200);
            Destroy(gameObject);
        }
    }
}
