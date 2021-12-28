using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    public int coins;
    public int score;
    public int level;
    public float time;
    public int lives;

    public Interface ui;

    Transform marioPlayer;
    SpriteRenderer marioSprite;
    public Animator marioAnimator;
    public GameObject marioObject;
    bool isDead;
    float deadStartPosition;

    //Win
    public bool winLevel = false;
    float winLevelAddScore = 0;

    private void Start()
    {
        marioPlayer = GameObject.Find("MarioPlayer").GetComponent<Transform>();
        marioSprite = GameObject.Find("MarioPlayer").GetComponent<SpriteRenderer>();

        if(PlayerPrefs.HasKey("coins") && PlayerPrefs.HasKey("score") && PlayerPrefs.HasKey("lives"))
        {
            coins = PlayerPrefs.GetInt("coins");
            score = PlayerPrefs.GetInt("score");
            lives = PlayerPrefs.GetInt("lives");
        }
        ui.SetAll(score, lives, (int)time);
    }

    public void AddCoins(int coins)
    {
        this.coins += coins;
    }

    public void AddScore(int score)
    {
        this.score += score;
        ui.SetScoreText(this.score);
    }

    public void LivesLoss()
    {
        this.lives--;
        ui.SetLiveText(lives);
    }

    public void Death()
    {
        LivesLoss();
        PlayerPrefs.SetInt("coins", coins);
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("lives", lives);
        deadStartPosition = marioPlayer.position.y;
        marioAnimator.SetBool("Dead", true);
        marioObject.GetComponent<Mario>().isAlive = false;
        marioObject.GetComponent<CapsuleCollider2D>().enabled = false;
        marioObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        marioObject.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        marioObject.transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
        isDead = true;
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
        }
    }

    public int GetLevel()
    {
        return level;
    }

    private void Update()
    {
        //Player Dead
        if (isDead)
        {
            marioPlayer.position += new Vector3(0, 1.5f * Time.deltaTime, 0);
            if(marioPlayer.position.y >= deadStartPosition + 1)
            {
                isDead = false;
                //Start level
                if(lives > 0)
                {
                    SceneManager.LoadScene("SampleScene");
                }
                else
                {
                    SceneManager.LoadScene("Menu");
                }
            }
        }

        //time to end
        if(time > 0 && !winLevel)
        {
            time -= Time.deltaTime;
            //ui.SetTimeText(time);
            if (time % 1.0 < 0.01f)
            {
                int intTime = (int)time;
                ui.SetTimeText(intTime);
            }
        }
        else if(time > -1 && !winLevel)
        {
            ui.SetTimeText(0);
            Debug.Log("DED");
            time = -1;
            Death();
        }
        else if (winLevel)
        {
            winLevelAddScore += Time.deltaTime;
            if(winLevelAddScore >= 0.04f && time > 0)
            {
                time--;
                ui.SetTimeText((int)time);
                AddScore(100);
                winLevelAddScore = 0;
            }
            else if(time <= 0)
            {
                winLevel = false;
                //Kolejny poziom <---
            }
        }
        
    }
}
