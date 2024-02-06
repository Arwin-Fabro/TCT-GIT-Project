using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public int Health = 100;
    int SlimeDmg = 2;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayerHeal();
            Debug.Log(Health);
        }
    }
    private void PlayerHurt()
    {
        Health -= 5;
    }
    private void PlayerHeal()
    {
        Health += 10;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Zombie")
        {
            PlayerHurt();
            Debug.Log(Health);
        }
        if (other.gameObject.tag == "Arrow")
        {
            PlayerHurt();
            Debug.Log(Health);
        }
        if (other.gameObject.tag == "Slime")
        {
            Health -= SlimeDmg;
            Debug.Log(Health);
        }
    }
    public void OnTriggerExit2D(Collider2D Enemy)
    {
        if (Enemy.gameObject.tag == "Slime")
        {
            Health -= SlimeDmg;
            Debug.Log(Health);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {

        Vector2 impulse = new Vector2(-4, 0);
        if (other.gameObject.CompareTag("Mimic"))
        {
            GetComponent<Rigidbody2D>().AddForce(impulse, ForceMode2D.Impulse);
        }
    }
}