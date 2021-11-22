using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    public Text scoreText;
    public Text coinText;
    Text liveText;
    public Text timeText;

    public void SetCoinText(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

}
