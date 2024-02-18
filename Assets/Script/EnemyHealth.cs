using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public GameObject thisEnemy; //The Whole Parent Object;
    public Slider slider;

    public int maxHealth = 150;
    public int currentHealth;

    private Animator animator;

    public int wizardDamage = 30;
    public static bool isDead = false;

    public float deathAnimationTime = 2f; // This is how much time your animation plays before destroying

    public AudioSource Audio;
    public AudioClip Hurt;
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
        Audio = gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        Dead();
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("isHurt");
        Audio.PlayOneShot(Hurt);
        currentHealth -= damage;
        slider.value = currentHealth;
    }
    public void Dead()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            animator.SetTrigger("isDead");
            Destroy(thisEnemy.gameObject, deathAnimationTime);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "WizardProjectile")
        {
            TakeDamage(wizardDamage);
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "WizardProjectile")
        {
            TakeDamage(wizardDamage);
        }
    }
}
