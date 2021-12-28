using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemyMother;
    public List<Vector2> position;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach(Vector2 pos in position)
            {
                Respawn(pos);
            }
        }
    }

    public void Respawn(Vector2 position)
    {
        Instantiate(enemy, position, Quaternion.identity, enemyMother.transform);
    }
}
