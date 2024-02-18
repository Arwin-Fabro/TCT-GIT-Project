using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFire : MonoBehaviour
{
    public Character characterScript;
    public EnemyHealth EH;
    public float speed;
    public AudioSource BossAudio;
    public AudioClip Fire;
    bool isInvulnerable;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            BossAudio.PlayOneShot(Fire);
            characterScript.GetComponent<Character>().TakeDamage(15);
            if (EH.currentHealth <= 200 && isInvulnerable == false)
            {
                StartCoroutine("OnInvulnerable");
                speed = 4f;
                characterScript.GetComponent<Character>().TakeDamage(10);
            }
        }
    }

    void Start()
    {
        BossAudio = gameObject.GetComponent<AudioSource>();
    }
    IEnumerator OnInvulnerable()
    {
        isInvulnerable = true;
        EH.currentHealth = 200;

        yield return new WaitForSeconds(1f); 

        EH.currentHealth = 200;
        isInvulnerable = false;
    }
}
