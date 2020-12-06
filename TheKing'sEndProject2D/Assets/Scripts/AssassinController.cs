﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AssassinController : MonoBehaviour
{
    [SerializeField] int health = 5;
    public int totalDeaths = 0;
    [SerializeField] int currentHealth;
    [SerializeField] int deathRewinds = 3;
    [SerializeField] int currentRewinds;
    [SerializeField] float movementSpeed = 4.0f;
    [SerializeField] float jumpForce = 7.5f;
    [SerializeField] float rollForce = 6.0f;
    [SerializeField] float gravityScale = 0.5f;
    [SerializeField] GameObject slideDust;
    [SerializeField] HealthController healthController;
    [SerializeField] TimeRewindController rewindController;
    [SerializeField] LevelComplete levelComplete;

    public AudioSource audioSource;
    public AudioClip jumpGrunt;
    public AudioClip hurtGrunt;
    public AudioClip bloodSquirt;
    public AudioClip deathSound;
    public AudioClip rewindSound;
    public AudioClip swordSlashSound;
    public AudioClip rollSound;

    private Animator animator;
    private Rigidbody2D rbody;
    private BoxCollider2D AssassinBoxCollider2D;
    private Transform AssassinDirection;
    private AssassinColliders assassinColliders;
    //private Transform spawnDustL;
    private Transform spawnDustR;
    private Transform mainCamera;

    private bool attackAnimationReady = true;
    private bool idleBlockAnimationReady = true;
    private bool extrajump = true;
    private bool rollingIsReady = true;
    private bool rollingAnimationIsReady = true;
    private bool rolling = false;
    private bool onWall = false;
    private bool isDead = false;
    private bool rewinding = false;
    private List<AssassinTransform> assassinTranforms;
    private float inputX;

    // Start is called before the first frame update
    void Start()
    {
       //healthController = GameObject.Find("HealthBar").GetComponent<HealthController>();
       // rewindController = GameObject.Find("ManaBar").GetComponent<TimeRewindController>();
       levelComplete = GameObject.Find("ScoreController").GetComponent<LevelComplete>();
        currentHealth = health;  
        currentRewinds = deathRewinds;
        healthController.SetMaxHealth(health);
        rewindController.SetMaxMana(deathRewinds);
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        AssassinBoxCollider2D = GetComponent<BoxCollider2D>();
        assassinTranforms = new List<AssassinTransform>();
        AssassinDirection = transform;
        assassinColliders = transform.Find("Assassin_Ground_Collider").GetComponent<AssassinColliders>();
        //spawnDustL = transform.Find("Assassin_Wall_Slide_L").GetComponent<Transform>();
        spawnDustR = transform.Find("Assassin_Wall_Slide_R").GetComponent<Transform>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");

        if (isDead == false)
        {

            if (rollingIsReady == true)
            {

                // Direction
                if (assassinColliders.SlidingState() == false && inputX > 0)
                {
                    AssassinDirection.localScale = new Vector2(1, 1);
                }
                else if (assassinColliders.SlidingState() == false && inputX < 0)
                {
                    AssassinDirection.localScale = new Vector2(-1, 1);
                }

                // Roll
                if (Input.GetKeyDown("left shift") && assassinColliders.GroundedState() == true)
                {
                    AssassinBoxCollider2D.enabled = false;
                    rollingIsReady = false;
                    rolling = true;
                    Invoke("RollCooldown", 1);
                    audioSource.PlayOneShot(rollSound);
                    rbody.AddForce(new Vector2(AssassinDirection.localScale.x * rollForce, rbody.velocity.y), ForceMode2D.Impulse);
                }

                // Horizontal movement
                if (rolling == false || assassinColliders.GroundedState() == true && assassinColliders.SlidingState() == true)
                {
                    //rbody.velocity = new Vector2(0, rbody.velocity.y);
                    rbody.velocity = new Vector2(inputX * movementSpeed, rbody.velocity.y);
                }
            }

            // Wall Sliding    
            if (onWall == false && assassinColliders.GroundedState() == false && assassinColliders.SlidingState() == true && rbody.velocity.y < 0)
            {
                //Left
                if (AssassinDirection.localScale.x == -1)
                {
                    Instantiate(slideDust, spawnDustR.position, Quaternion.Euler(gameObject.transform.rotation.x, 180, gameObject.transform.rotation.z));
                }
                //Right
                else if (AssassinDirection.localScale.x == 1)
                {
                    Instantiate(slideDust, spawnDustR.position, gameObject.transform.localRotation);
                }

                rbody.gravityScale = gravityScale;
                onWall = true;
                extrajump = true;
            }
            else if (onWall == true && assassinColliders.SlidingState() == false)
            {
                rbody.gravityScale = gravityScale * 2;
                onWall = false;
            }
            else if (onWall == true && assassinColliders.GroundedState() == true)
            {
                rbody.gravityScale = gravityScale * 2;
                onWall = false;
            }

            // Jump
            if (Input.GetKeyDown(KeyCode.W) && assassinColliders.GroundedState() == true || Input.GetKeyDown(KeyCode.W) && extrajump == true)
            {
                audioSource.PlayOneShot(jumpGrunt);
                rbody.gravityScale = gravityScale * 2;
                rbody.velocity = new Vector2(rbody.velocity.x, jumpForce);
                extrajump = false;
            }

            // Attack
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Attack");
                audioSource.PlayOneShot(swordSlashSound);
}

            // Block
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Block");
            }

            // Counter attack on perfect block      

            // Jump animation 
            if (rbody.velocity.y == 0 || assassinColliders.GroundedState() == true)
            {
                animator.SetBool("Fall", false);
            }
            else
            {
                if (rbody.velocity.y > 0)
                {
                    
                    animator.SetBool("Jump", true);
                }

                else if (rbody.velocity.y < 0)
                {
                    animator.SetBool("Jump", false);
                    animator.SetBool("Fall", true);
                }
              
            }

            // Run animation
            if (assassinColliders.GroundedState() == true)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    animator.SetBool("Run", true);
                }
                else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
                {
                    animator.SetBool("Run", false);
                }
            }
            else if (assassinColliders.GroundedState() == false)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    animator.SetBool("Run", true);
                    animator.SetBool("Fall", false);
                }
                else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
                {
                    animator.SetBool("Run", false);
                }

            }

            // Wall slide animation
            if (assassinColliders.SlidingState() == true && assassinColliders.GroundedState() == true)
            {
                animator.SetBool("Wall Slide", false);
            }
            else if (assassinColliders.SlidingState() == true)
            {
                animator.SetBool("Wall Slide", true);
            }
            else if (assassinColliders.SlidingState() == false)
            {
                animator.SetBool("Wall Slide", false);
            }

            // Roll animation
            if (rollingAnimationIsReady == true && Input.GetKeyUp("left shift") && assassinColliders.GroundedState() == true)
            {
                rollingAnimationIsReady = false;
                animator.SetTrigger("Roll");
                Invoke("RollAnimationCooldown", 1);
            }

            // Attack animations
            if (attackAnimationReady == true && Input.GetMouseButtonDown(0))
            {
                attackAnimationReady = false;
                AttackAnimations(Random.Range(0, 3));
                Invoke("AttackAnimationCooldown", 1);
            }

            // Idle block animation
            if (idleBlockAnimationReady == true && Input.GetMouseButtonDown(1))
            {
                idleBlockAnimationReady = false;
                animator.SetTrigger("Idle Block");
                Invoke("IdleBlockAnimationCooldown", 1);
            }

            // Block attack animation
        }
        // Death Rewind
        else if (isDead == true && deathRewinds != 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AssassinBoxCollider2D.enabled = false;
                animator.SetBool("Rewind", true);
                rewinding = true;
                audioSource.PlayOneShot(rewindSound);
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                AssassinBoxCollider2D.enabled = true;
                animator.SetBool("Rewind", false);
                rewinding = false;
                assassinTranforms.Clear();
                if (deathRewinds > 0)
                {
                    deathRewinds--;
                    totalDeaths++;
                    levelComplete.getNoOfDeaths(totalDeaths);

                    rewindController.SetMana(currentRewinds);
                    isDead = false;
                    currentHealth = health;
                }
            }
            animator.SetBool("Run", false);
        }
    }

    private void FixedUpdate()
    {
        mainCamera.position = new Vector3(transform.position.x, transform.position.y, -10);

        if (isDead == false)
        {
            assassinTranforms.Add(new AssassinTransform(transform.position.x, transform.position.y, transform.position.z, transform.localScale.x));
        }
        else if (isDead == true && rewinding == true && assassinTranforms.Count - 1 > 0)
        {

            healthController.SetMaxHealth(health);
            

            transform.position = new Vector3(assassinTranforms[assassinTranforms.Count - 1].positionX, assassinTranforms[assassinTranforms.Count - 1].positionY, assassinTranforms[assassinTranforms.Count - 1].positionZ);
            transform.localScale = new Vector3(assassinTranforms[assassinTranforms.Count - 1].localScaleX, 1, 1);
            assassinTranforms.RemoveAt(assassinTranforms.Count - 1);
        }
    }

    void RollCooldown()
    {
        AssassinBoxCollider2D.enabled = true;
        rollingIsReady = true;
        rolling = false;
    }

    void RollAnimationCooldown()
    {
        rollingAnimationIsReady = true;
    }

    void AttackAnimationCooldown()
    {
        attackAnimationReady = true;
    }

    void AttackAnimations(int num)
    {
        if (num == 0)
        {
            animator.SetTrigger("Attack 1");
        }
        else if (num == 1)
        {
            animator.SetTrigger("Attack 2");
        }
        else if (num == 2)
        {
            animator.SetTrigger("Attack 3");
        }
    }

    void IdleBlockAnimationCooldown()
    {
        idleBlockAnimationReady = true;
    }

    public void HurtPlayer()
    {
        if (currentHealth - 1 > 0)
        {
            currentHealth--;
            healthController.SetHealth(currentHealth);
            animator.SetTrigger("Hurt");
            audioSource.PlayOneShot(hurtGrunt);
            audioSource.PlayOneShot(bloodSquirt);
        }
        else if (currentHealth - 1 == 0)
        {
            currentHealth--;
            healthController.SetHealth(currentHealth);
            animator.SetTrigger("Rewind Death");
            isDead = true;
            audioSource.PlayOneShot(deathSound);


            if (deathRewinds == 0)
            {
                animator.SetTrigger("Permanent Death");
                Invoke("RestScene", 3);
            }
        }
    }

    private void RestScene()
    {


        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    internal class AssassinTransform
    {
        public float positionX { get; set; }
        public float positionY { get; set; }
        public float positionZ { get; set; }
        public float localScaleX { get; set; }

        public AssassinTransform(float x, float y, float z, float scaleZ)
        {
            this.positionX = x;
            this.positionY = y;
            this.positionZ = z;
            this.localScaleX = scaleZ;
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetDeathRewinds()
    {
        return deathRewinds;
    }
}
