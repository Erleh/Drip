using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // what to do when the enemy reaches the player
    void OnAttack(GameObject player)
    {
        print("Attacked");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            OnAttack(col.gameObject);
        }
    }
}
