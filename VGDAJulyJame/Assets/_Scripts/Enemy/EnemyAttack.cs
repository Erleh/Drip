using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    bool attacked = false;

    // what to do when the enemy reaches the player
    void OnAttack(GameObject player)
    {
        if (!attacked)
        {
            //print("Attacked");
            attacked = true;
            player.GetComponent<PlayerState>().OnAttacked();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            OnAttack(col.gameObject);
        }
    }
}
