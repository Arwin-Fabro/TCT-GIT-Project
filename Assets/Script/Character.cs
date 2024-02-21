using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public int moveSpeed = 10;
    public Rigidbody2D rb;
    public BoxCollider2D BC;
    public Animator animator;

    public bool canDash = true;
    public bool isDashing = false;
    public float dashingPower = 2f;
    public float dashingTime = 0.2f;
    public float dashingCoolDown = 1f;
    [SerializeField] private TrailRenderer tr;

    public bool CombatMode = true;
    public bool GravityMode = false;
    public bool PortalMode = false;

    public Image CombatCD;
    public Image DashCD;

    public GameObject EnergyShield;
    public GameObject EnergyShieldPos;
    public Transform WizardProjectilePos;
    public GameObject WizardProjectile;

    public bool canFire;
    private float fireballTimer;
    public float TimeBetweenFiring = 1f;

    public float groundcheckradius;
    public LayerMask ground;
    private bool OnGround;
    public Transform groundcheck;
    public int MaxJumps = 2;
    public int NumberOfJumps;
    public float JumpForce = 250;

    public float maxHealth = 100;
    public float currentHealth;

    public float maxMana = 80f;
    public float currentMana;

    public HealthBar healthBar;
    public ManaBar manaBar;

    private bool isDead = false;

    public Text infoText;
    public GameObject infoTxt;
    public bool infoDone = true;

    


    public bool facingRight = true;

    public PickUp gravity;
    public string GameOverScene = "GameOver";
    
    // Start is called before the first frame update
    void Start()
    {
        CombatCD.fillAmount = 0;
        DashCD.fillAmount = 0;
        CombatMode = true;
        GravityMode = false;
        PortalMode = false;
    NumberOfJumps = MaxJumps;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);
        InvokeRepeating("ManaRegeneration", 5f, 0.5f);
        InvokeRepeating("HealthRegeneration", 5f, 1f);
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
        StartCoroutine(SwitchModes());
        Death();
    }
    IEnumerator SwitchModes()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && infoDone)
        {
            CombatMode = true;
            GravityMode = false;
            PortalMode = false;
            infoText.text = "Combat Mode";
            infoDone = false;
            infoTxt.SetActive(true);
            yield return new WaitForSeconds(5f);
            infoDone = true;
            infoTxt.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && infoDone)
        {
            CombatMode = false;
            GravityMode = true;
            PortalMode = false;
            infoText.text = "Gravity Mode";
            infoDone = false;
            infoTxt.SetActive(true);
            yield return new WaitForSeconds(5f);
            infoDone = true;
            infoTxt.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && infoDone)
        {
            CombatMode = false;
            GravityMode = false;
            PortalMode = true;
            infoText.text = "Portal Mode";
            infoDone = false;
            infoTxt.SetActive(true);
            yield return new WaitForSeconds(5f);
            infoDone = true;
            infoTxt.SetActive(false);
        }
    }
    IEnumerator Attack()
    {
        //Fire Rate Intervals
        if (!canFire)
        {
            CombatCD.fillAmount -= 1 / TimeBetweenFiring * Time.deltaTime;
            if (CombatCD.fillAmount <= 0)
            {
                CombatCD.fillAmount = 0;
            }
            fireballTimer += Time.deltaTime;
            if (fireballTimer > TimeBetweenFiring)
            {
                canFire = true;
                fireballTimer = 0;
            }
        }



        // Ability 1 (Combat Mode) : Shoot Fireball and Shockwave
        if (Input.GetMouseButtonDown(0) && CombatMode && currentMana >=5 && canFire)
        {
            canFire = false;
            CombatCD.fillAmount = 1;
            currentMana -= 5;
            manaBar.SetMana(currentMana);
            animator.SetTrigger("Attack2");
            yield return new WaitForSeconds(0.3f);
            GameObject temp = Instantiate(WizardProjectile, WizardProjectilePos.transform.position, Quaternion.identity) as GameObject;
            Destroy(temp, 5f);
        }
        else if (Input.GetMouseButtonDown(0) && currentMana <= 5 && infoDone)
        {
            infoDone = false;
            infoText.text = "Not Enough Mana";
            infoTxt.SetActive(true);
            yield return new WaitForSeconds(5f);
            infoTxt.SetActive(false);
            infoDone = true;
        }
        if ((Input.GetMouseButtonDown(1) && CombatMode && currentMana >= 30 && canFire))
        {
            canFire = false;
            CombatCD.fillAmount = 1;
            currentMana -= 30;
            manaBar.SetMana(currentMana);
            animator.SetTrigger("Attack1");
            yield return new WaitForSeconds(0.3f);
            GameObject temp2 = Instantiate(EnergyShield, EnergyShieldPos.transform.position, Quaternion.identity) as GameObject;
            Destroy(temp2, 5f);
        }
        else if (Input.GetMouseButtonDown(1) && currentMana <= 30 && infoDone)
        {
            infoDone = false;
            infoText.text = "Not Enough Mana";
            infoTxt.SetActive(true);
            yield return new WaitForSeconds(5f);
            infoTxt.SetActive(false);
            infoDone = true;
        }

        //Ability 2 (Gravity Mode): Drag Box;
        if (Input.GetMouseButtonDown(0) && GravityMode)
        {
            gravity.MUD();
        }
        if (Input.GetMouseButtonUp(0) && GravityMode)
        {
            gravity.MUP();
        }
        
        //Ability 3(Portal Mode): Shoot Portals
        if (Input.GetMouseButtonDown(0) && PortalMode)
        {
            // Shoot portal1
        }
        if (Input.GetMouseButtonDown(1) && PortalMode)
        {
            // Shoot portal1
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
            this.rb.AddForce(new Vector2(0, JumpForce));
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
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Key")
        {
            Door.Keys += 1;
            Destroy(other.gameObject);
        }
    }
    void ManaRegeneration()
    {
        currentMana += 1f;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        manaBar.SetMana(currentMana);

    }
    void HealthRegeneration()
    {
        currentHealth += 1f;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);
    }
    void FixedUpdate()
    {
        OnGround = Physics2D.OverlapCircle(groundcheck.position, groundcheckradius, ground);
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("isHurt");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void Death()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            rb.isKinematic = false;
            BC.enabled = false;
            //rb.gravityScale = 0;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            CancelInvoke("HealthRegeneration");
            animator.SetTrigger("isDead");
            Invoke("GameOver", 3f);


        }
    }
    void GameOver()
    {
        SceneManager.LoadScene(GameOverScene);
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
        DashCD.fillAmount = 1;
        DashCD.fillAmount -= 1 / dashingCoolDown * Time.deltaTime;
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
