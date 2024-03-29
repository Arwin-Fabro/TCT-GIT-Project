﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;

    private float timer;
    private GameObject player;

    private Animator animator;
    public bool facingRight;

    public AudioSource Archer;
    public AudioClip Shoot;
    public EnemyHealth enemyHealth;
    public Character characterScript;

    // Start is called before the first frame update
    void Start()
    {
        characterScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        Archer = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 10)
        {
            timer += Time.deltaTime;
        }
        if (timer > 2)
        {
            timer = 0;
            if (enemyHealth.GetComponent<EnemyHealth>().isDead == false && characterScript.GetComponent<Character>().isDead == false)
            {
                StartCoroutine(shoot());
            }
        }
        if (player.transform.position.x >= this.gameObject.transform.position.x && facingRight)
        {
            flip(); //turn left
        }
        if (player.transform.position.x <= this.gameObject.transform.position.x && !facingRight)
        {
            flip(); //turn right
        }
    }

    private IEnumerator shoot()
    {
        animator.SetTrigger("isShoot");
        Archer.PlayOneShot(Shoot);
        yield return new WaitForSeconds(1.2f);
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
    void flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
