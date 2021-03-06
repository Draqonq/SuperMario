using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Type of block
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
    public GameObject boost;

    bool isBreaking = false;
    bool movesUp = true;
    bool isEmptyBlock = false;
    Vector3 startPosition;
    //Vector3 transformSpeed = new Vector3(0, 0.0125f, 0);
    Vector3 transformSpeed = new Vector3(0, 0.05f, 0);

    PlayerData playerData;

    private void Start()
    {
        playerData = GameObject.Find("Data").GetComponent<PlayerData>();
    }

    void FixedUpdate()
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
                if(blockType == BlockType.MushroomBlock || (blockType == BlockType.CoinBlock && coins <= 0) || (blockType == BlockType.CoinBrick && coins <= 0))
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
            case BlockType.ClassicBrick:
                {
                    if (playerData.GetLevel() > 1)
                    {
                        HealthLoss();
                    }
                }
                break;
            case BlockType.CoinBrick:
                {
                    if (playerData.GetLevel() > 1)
                    {
                        DropCoin();
                    }
                }
                break;
            case BlockType.CoinBlock:
                {
                    DropCoin();
                    if(coins <= 0)
                    {
                        ChangeBlockSprite();
                    }
                }
                break;
            case BlockType.MushroomBlock:
                {
                    DropMushroom();
                    ChangeBlockSprite();
                }
                break;
        }
    }

    void HealthLoss()
    {
        blockHealth--;
        if(blockHealth <= 0)
        {
            playerData.AddScore(50);
            Destroy(gameObject);
        }
    }

    void DropCoin()
    {
        coins--;
        playerData.AddScore(200);
        playerData.AddCoins(1);
        //drop coin + animation
        Instantiate(coin, (transform.position + new Vector3(0,0,1)), Quaternion.identity);
        if(coins <= 0)
        {
            ChangeBlockSprite();
        }
    }

    void DropMushroom()
    {
        //drop mushroom + animation
        Instantiate(boost, (transform.position + new Vector3(0, 0.2f, 1)), Quaternion.identity);
    }

    void ChangeBlockSprite()
    {
        GetComponent<SpriteRenderer>().sprite = emptyBlock;
    }
}
