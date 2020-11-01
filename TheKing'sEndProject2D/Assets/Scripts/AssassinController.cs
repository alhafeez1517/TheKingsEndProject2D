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

    private Rigidbody2D rbody;
    private Transform AssassinDirection;
    private AssassinColliders assassinColliders;

    private bool extrajump = true;
    private bool rollingIsReady = true;
    private bool rolling = false;
    private bool onWall = false;
    private float inputX;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        AssassinDirection = transform;
        assassinColliders = transform.Find("Assassin_Ground_Collider").GetComponent<AssassinColliders>();
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
            if (AssassinDirection.localScale.x == 1)
            {
                AssassinDirection.localScale = new Vector2(-1, 1);
            }
            else if (AssassinDirection.localScale.x == -1)
            {
                AssassinDirection.localScale = new Vector2(1, 1);
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
            if (AssassinDirection.localScale.x == 1)
            {
                AssassinDirection.localScale = new Vector2(-1, 1);
            }
            else if (AssassinDirection.localScale.x == -1)
            {
                AssassinDirection.localScale = new Vector2(1, 1);
            }
            rbody.gravityScale = gravityScale * 2;
            onWall = false;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.W) && assassinColliders.GroundedState() || Input.GetKeyDown(KeyCode.W) && extrajump == true)
        {
            rbody.gravityScale = gravityScale * 2;
            rbody.velocity = new Vector2(rbody.velocity.x, jumpForce);
            extrajump = false;
        }

        // Attack
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attack");
        }

        // Block
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Block");
        }

        // Counter attack on perfect block
    }

    void RollCooldown()
    {
        rollingIsReady = true;
        rolling = false;
    }
}
