using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
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
        else if (transform.position.y >= (startPosition.y + 1))
        {
            Destroy(gameObject);
        }
    }

}
