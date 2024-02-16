using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    public GameObject PointA;
    public GameObject PointB;
    private Rigidbody2D rb;
    private Transform currentpoint;
    public float speed;
    public float range;
    public Transform Player;
    public Animator WarriorAnim;
    public Character characterScript;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentpoint = PointB.transform;
        characterScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        WarriorAnim = GetComponent<Animator>();
        WarriorAnim.SetBool("isIdle", true);
        WarriorAnim.SetBool("isRunning", false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentpoint.position - transform.position;
        if(currentpoint == PointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
            WarriorAnim.SetBool("isIdle", false);
           
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
            WarriorAnim.SetBool("isIdle", false);
        }

        if(Vector2.Distance(transform.position, currentpoint.position)< 0.5f && currentpoint == PointB.transform)
        {
            flip();
            currentpoint = PointA.transform;
        }
        if (Vector2.Distance(transform.position, currentpoint.position) < 0.5f && currentpoint == PointA.transform)
        {
            flip();
            currentpoint = PointB.transform;
        }
        if(EnemyHealth.isDead == true)
        {
            speed = 0;
            WarriorAnim.SetBool("isRunning", false);
        }
    }
    void FixedUpdate()
    {
        if (Vector3.Distance(Player.position, transform.position) <= range)

        {
            speed = 1;
            WarriorAnim.SetBool("isAttacking", true);
            WarriorAnim.SetBool("isRunning", false);
            transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
        }
        if(Vector3.Distance(Player.position, transform.position) > range)
        {
            speed = 3;
            WarriorAnim.SetBool("isRunning", true);
            WarriorAnim.SetBool("isAttacking", false);
            transform.position = Vector2.MoveTowards(transform.position, currentpoint.position, speed * Time.deltaTime);
        }

    }
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);
        Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            characterScript.GetComponent<Character>().TakeDamage(10);
            
        }
    }
}
