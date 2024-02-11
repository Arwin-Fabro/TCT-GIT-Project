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

    public bool CombatMode = true;
    public bool GravityMode = false;

    public GameObject EnergyShield;
    public GameObject EnergyShieldPos;
    public GameObject WizardProjectilePos;
    public GameObject WizardProjectile;

    public float groundcheckradius;
    public LayerMask ground;
    private bool OnGround;
    public Transform groundcheck;
    public int MaxJumps = 2;
    public int NumberOfJumps;

    public int maxHealth = 100;
    public int currentHealth;

    public float maxMana = 80f;
    public float currentMana;

    public HealthBar healthBar;
    public ManaBar manaBar;

    private bool isDead = false;


    public bool facingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        CombatMode = true;
        GravityMode = false;
        NumberOfJumps = MaxJumps;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);
        InvokeRepeating("ManaRegeneration", 5f, 0.5f);
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
        StartCoroutine(Attack());
        Movement();
        SwitchModes();
        Death();
    }
    void SwitchModes()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CombatMode = true;
            GravityMode = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CombatMode = false;
            GravityMode = true;
        }
    }
    IEnumerator Attack()
    {
        if (Input.GetMouseButtonDown(0) && CombatMode && currentMana >=5)
        {
            currentMana -= 5;
            manaBar.SetMana(currentMana);
            animator.SetTrigger("Attack2");
            yield return new WaitForSeconds(0.3f);
            GameObject temp = Instantiate(WizardProjectile, WizardProjectilePos.transform.position, Quaternion.identity) as GameObject;
            Destroy(temp, 5f);
        }
        if ((Input.GetMouseButtonDown(1) && CombatMode && currentMana >= 30))
        {
            currentMana -= 30;
            manaBar.SetMana(currentMana);
            animator.SetTrigger("Attack1");
            yield return new WaitForSeconds(0.3f);
            GameObject temp2 = Instantiate(EnergyShield, EnergyShieldPos.transform.position, Quaternion.identity) as GameObject;
            Destroy(temp2, 5f);
        }
        if (Input.GetMouseButtonDown(0) && GravityMode)
        {
            animator.SetTrigger("Attack1");
        }
        if (Input.GetMouseButtonDown(1) && GravityMode)
        {
            //Gravity Spell
        }
    }
    void Movement()
    {
        if (isDashing)
        {
            return;
        }
        //Move left and right
        if (Input.GetKey(KeyCode.D ) && isDead == false)
        {
            if (!facingRight && isDashing == false)
            {
                Flip();
            }
            animator.SetBool("isRun", true);
            transform.position += transform.right * moveSpeed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.A) && isDead == false)
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
        if (Input.GetKeyDown(KeyCode.Space) && NumberOfJumps >0 && isDead == false)
        {

            NumberOfJumps--;
            animator.SetTrigger("isJump");
            this.rb.AddForce(new Vector2(0, 250));
        }
        if (OnGround)
        {
            NumberOfJumps = MaxJumps;
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }


    }
    void ManaRegeneration()
    {
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        currentMana += 1f;
        manaBar.SetMana(currentMana);
        
        

    }
    void FixedUpdate()
    {
        OnGround = Physics2D.OverlapCircle(groundcheck.position, groundcheckradius, ground);
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("isHurt");
        print("Hurt");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void Death()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            animator.SetTrigger("isDead");
            
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
