using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlow : MonoBehaviour
{
    private Rigidbody2D rb;
    private Character player;
    private float MoveSpeed;
    private Vector3 directionToPlayer;
    private Vector3 localScale;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType(typeof(Character)) as Character;
        MoveSpeed = 1f;
        localScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        MoveEnemy();
    }
    private void MoveEnemy()
    {
        directionToPlayer = (player.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(directionToPlayer.x, directionToPlayer.y) * MoveSpeed;
    }
    private void LateUpdate()
    {
        if(rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
        }
        else if(rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        }
    }
}
