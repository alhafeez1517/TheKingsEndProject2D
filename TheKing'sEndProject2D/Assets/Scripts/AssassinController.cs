using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinController : MonoBehaviour
{
    [SerializeField] float Health = 100.0f;
    [SerializeField] float movementSpeed = 4.0f;
    [SerializeField] float jumpForce = 7.5f;
    [SerializeField] float rollForce = 6.0f;
    [SerializeField] float gravityScale = 0.5f;
    [SerializeField] GameObject slideDust;

    private Animator animator;
    private Rigidbody2D rbody;
    private Transform AssassinDirection;
    private AssassinColliders assassinColliders;
    private Transform spawnDustR;

    private bool attackAnimationReady = true;
    private bool idleBlockAnimationReady = true;
    private bool extrajump = true;
    private bool rollingIsReady = true;
    private bool rollingAnimationIsReady = true;
    private bool rolling = false;
    private bool onWall = false;
    private float inputX;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        AssassinDirection = transform;
        assassinColliders = transform.Find("Assassin_Ground_Collider").GetComponent<AssassinColliders>();
        spawnDustR = transform.Find("Assassin_Wall_Slide_R").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");

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
                rollingIsReady = false;
                rolling = true;
                Invoke("RollCooldown", 1);
                rbody.AddForce(new Vector2(AssassinDirection.localScale.x * rollForce, rbody.velocity.y), ForceMode2D.Impulse);
            }

            // Horizontal movement
            if (rolling == false || assassinColliders.GroundedState() == true && assassinColliders.SlidingState() == true)
            {
                rbody.velocity = new Vector2(0, rbody.velocity.y);
                rbody.velocity = new Vector2(inputX * movementSpeed, rbody.velocity.y);
            }
        }

        // Wall Sliding    
        if (onWall == false && assassinColliders.GroundedState() == false && assassinColliders.SlidingState() == true && rbody.velocity.y < 0)
        {
            if (AssassinDirection.localScale.x == -1)
            {
                Instantiate(slideDust, spawnDustR.position, gameObject.transform.localRotation);
            }
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
            rbody.gravityScale = gravityScale * 2;
            rbody.velocity = new Vector2(rbody.velocity.x, jumpForce);
            extrajump = false;
        }

        // Attack
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("Attack");
        //}

        // Block
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Block");
        }

        // Counter attack on perfect block      

        // Jump animation 
        if (rbody.velocity.y > 0)
        {
            animator.SetBool("Jump", true);
        }
        else if (rbody.velocity.y < 0)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", true);
        }
        else if (rbody.velocity.y == 0)
        {
            animator.SetBool("Fall", false);
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
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
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
        if (rollingAnimationIsReady == true && Input.GetKeyUp("left shift"))
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

    void RollCooldown()
    {
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
}
