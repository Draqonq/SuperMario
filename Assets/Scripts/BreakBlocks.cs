using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    ClassicBrick, CoinBrick, CoinBlock, MushroomBlock 
}

public class BreakBlocks : MonoBehaviour
{
    public int blockHealth;
    public int coins;
    public BlockType blockType;
    public Sprite emptyBlock;
    public GameObject coin;

    bool isBreaking = false;
    bool movesUp = true;
    bool isEmptyBlock = false;
    Vector3 startPosition;
    Vector3 transformSpeed = new Vector3(0, 0.0125f, 0);
    void Update()
    {
        if (isBreaking && !isEmptyBlock)
        {
            //Up
            if(transform.position.y < startPosition.y + 0.5f && movesUp)
            {
                transform.position += transformSpeed;
            }
            else if(transform.position.y >= startPosition.y + 0.5f && movesUp)
            {
                movesUp = false;
                CheckBlock();
            }
            else if(transform.position.y > startPosition.y && !movesUp)
            {
                transform.position -= transformSpeed;
            }
            else if(transform.position.y <= startPosition.y)
            {
                transform.position = startPosition;
                isBreaking = false;
                movesUp = true;
                if(blockType == BlockType.MushroomBlock || (blockType == BlockType.CoinBlock && coins <= 0))
                {
                    isEmptyBlock = true;
                }
            }
        }
    }

    public void BreakBlock()
    {
        if (!isBreaking)
        {
            startPosition = transform.position;
            isBreaking = true;
        }
    }

    void CheckBlock()
    {
        switch (blockType)
        {
            case BlockType.ClassicBrick: HealthLoss(); break;
            case BlockType.CoinBrick:
                {
                    DropCoin();
                    HealthLoss();
                    break;
                }
            case BlockType.CoinBlock:
                {
                    DropCoin();
                    ChangeBlockSprite();
                    break;
                }
            case BlockType.MushroomBlock:
                {
                    DropMushroom();
                    ChangeBlockSprite(); 
                    break;
                }
        }
    }

    void HealthLoss()
    {
        blockHealth--;
        if(blockHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void DropCoin()
    {
        //coins--;
        //drop coin + animation
        Instantiate(coin, (transform.position + new Vector3(0,0,1)), Quaternion.identity);
        Debug.Log("Coin");
    }

    void DropMushroom()
    {
        //drop mushroom + animation
        Debug.Log("Mushroom");
        GetComponent<SpriteRenderer>().sprite = emptyBlock;
    }

    void ChangeBlockSprite()
    {
        GetComponent<SpriteRenderer>().sprite = emptyBlock;
    }
}
