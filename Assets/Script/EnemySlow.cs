using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlow : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject player;
    private float MoveSpeed;
    public bool Flip;
    public float groundcheckradius;
    public LayerMask ground;
    private bool OnGround;
    public Transform groundcheck;
    public Animator SlimeAnim;
    public Character characterScript;
    public GameObject Slime;
    public AudioSource SlimeAudio;
    public AudioClip SlimeHit;
    public AudioClip SlimeMove;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SlimeAnim = GetComponent<Animator>();
        MoveSpeed = 1f;
        SlimeAnim.SetBool("isIdle", true);
        SlimeAnim.SetBool("isWalking", false);
        SlimeAudio = gameObject.GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        
        OnGround = Physics2D.OverlapCircle(groundcheck.position, groundcheckradius, ground);
    }
    private void Update()
    {
        float distance = Vector2.Distance(this.transform.position, player.transform.position);
        if(distance <= 15f)
        {
            FollowPlayer();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            characterScript.GetComponent<Character>().TakeDamage(10);
            SlimeAudio.PlayOneShot(SlimeHit);
        }
    }
    void FollowPlayer()
    {
        Vector3 scale = transform.localScale;
        if (player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (Flip ? -1 : 1);
            transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);
            SlimeAnim.SetBool("isIdle", false);
            SlimeAnim.SetBool("isWalking", true);
            
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (Flip ? -1 : 1);
            transform.Translate(MoveSpeed * Time.deltaTime * -1, 0, 0);
            SlimeAnim.SetBool("isIdle", false);
            SlimeAnim.SetBool("isWalking", true);
            
        }
        transform.localScale = scale;
    }
}
