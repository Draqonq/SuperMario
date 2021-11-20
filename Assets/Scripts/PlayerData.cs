using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int coins;
    public int time;
    public int lives;

    public void AddCoins(int coins)
    {
        this.coins += coins;
    }

    public void Death()
    {
        this.lives--;
    }

    private void Update()
    {
        //time to end
    }
}
