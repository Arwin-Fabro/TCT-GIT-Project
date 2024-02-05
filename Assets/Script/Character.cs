using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int moveSpeed = 10;
    public Rigidbody2D rb;
    public Animator animator;

    public bool canDash = true;
    public bool isDashing = false;
    public float dashingPower = 2f;
    public float dashingTime = 0.2f;
    public float dashingCoolDown = 1f;
    [SerializeField] private TrailRenderer tr;

    public bool facingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (isDashing)
        {
            return;
        }

        Movement();
        Attack();
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack1");
        }
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Attack2");
        }
    }
    void Movement()
    {
        if (isDashing)
        {
            return;
        }
        //Move left and right
        if (Input.GetKey(KeyCode.D ))
        {
            if (!facingRight && isDashing == false)
            {
                Flip();
            }
            animator.SetBool("isRun", true);
            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (facingRight && isDashing == false)
            {
                Flip();
            }
            animator.SetBool("isRun", true);
            transform.position += transform.right * moveSpeed * Time.deltaTime * -1;
        }
        else if (Input.anyKey == false)
        {
            animator.SetBool("isRun", false);
        }
        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("isJump");
            this.rb.AddForce(new Vector2(0, 250));
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }


    }


    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }

}
