using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Moneta która pojawia siê po zniszczeniu bloku


public class Coin : MonoBehaviour
{
    public float dropCoinSpeed = 0.01f;

    Vector3 dropCoinVector;
    Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
        dropCoinVector = new Vector3(0, dropCoinSpeed);
    }

    private void Update()
    {
        DropCoin();
    }

    void DropCoin()
    {
        //Up
        if (transform.position.y < (startPosition.y + 1))
        {
            transform.position += dropCoinVector;
        }
        //Destroy
        else if (transform.position.y >= (startPosition.y + 1))
        {
            Destroy(gameObject);
        }
    }

}
