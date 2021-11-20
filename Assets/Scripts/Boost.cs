using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    PlayerData playerData;
    public Vector3 dropBoostSpeed = new Vector3(0, 0.01f);
    Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
        playerData = GameObject.Find("Data").GetComponent<PlayerData>();
    }

    private void Update()
    {
        DropBoost();
    }

    void DropBoost()
    {
        if (transform.position.y < (startPosition.y + .5f))
        {
            transform.position += dropBoostSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Boost");
            //playerData. UpgradeCharacter
            Destroy(gameObject);
        }
    }
}
