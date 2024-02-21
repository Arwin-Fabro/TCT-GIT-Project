using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldborder : MonoBehaviour
{
    public Character Player;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player.currentHealth = 0;
            Player.healthBar.SetHealth(Player.currentHealth);
            Player.Death();
        }
    }
}
