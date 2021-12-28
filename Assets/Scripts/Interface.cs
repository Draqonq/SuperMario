using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    public Text scoreText;
    public Text liveText;
    public Text timeText;

    public void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SetLiveText(int live)
    {
        liveText.text = live.ToString();
    }

    public void SetTimeText(int time)
    {
        timeText.text = time.ToString();
    }

    public void SetAll(int score, int live, int time)
    {
        scoreText.text = score.ToString();
        liveText.text = live.ToString();
        timeText.text = time.ToString();
    }

}
