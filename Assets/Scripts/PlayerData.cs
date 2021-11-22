using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int coins;
    public int score;
    public int level;
    public int time;
    public int lives;

    public Interface ui;

    Transform marioPlayer;
    SpriteRenderer marioSprite;

    private void Start()
    {
        marioPlayer = GameObject.Find("MarioPlayer").GetComponent<Transform>();
        marioSprite = GameObject.Find("MarioPlayer").GetComponent<SpriteRenderer>();
    }

    public void AddCoins(int coins)
    {
        this.coins += coins;
        ui.SetCoinText(this.coins);
    }

    public void AddScore(int score)
    {
        this.score += score;
        ui.SetScoreText(this.score);
    }

    public void Death()
    {
        this.lives--;
    }

    public void LevelUp()
    {
        this.level++;
        AddScore(1000);
        SetLevel();
    }

    public void LevelDown()
    {
        this.level--; 
        SetLevel();
    }

    void SetLevel()
    {
        if(level == 1)
        {
            marioPlayer.localScale = new Vector2(1, 1);
            marioSprite.color = Color.white;
            //sprite normal
        }
        else if (level == 2)
        {
            marioPlayer.localScale = new Vector2(1, 2);
            marioSprite.color = Color.white;
            //sprite normal
        }
        else if (level >= 3)
        {
            marioPlayer.localScale = new Vector2(1, 2);
            marioSprite.color = Color.red;
            //sprite white
        }
        else if (level <= 0)
        {
            Death();
            Debug.Log("Dead");
        }
    }

    public int GetLevel()
    {
        return level;
    }

    private void Update()
    {
        //time to end
    }
}
