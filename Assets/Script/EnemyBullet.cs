using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;

    public float force;

    private float Timer;

    public Character characterScript;

    // Start is called before the first frame update
    void Start()
    {
        characterScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 180);
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            characterScript.GetComponent<Character>().TakeDamage(20);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
