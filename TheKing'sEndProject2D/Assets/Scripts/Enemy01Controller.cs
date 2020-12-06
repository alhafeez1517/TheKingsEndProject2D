using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Controller : MonoBehaviour
{
    //Adjustables
    [SerializeField] int enemyHealth = 3;
    [SerializeField] int enemyCurrentHealth;
    [SerializeField] float enemySpeed = 3f;
    //If linear distance > 10, state=idle.
    [SerializeField] float invokePatrolDist = 10f;
    //If 10 > linear distance > 5, state=patrol(Wanders).
    [SerializeField] float invokeChaseDist = 5f;
    //If linear distance < 5, state=chase(Follows player).
    [SerializeField] float invokeAttackDist = 3f;
    //Public parameters.
    public int enemyState;
    public GameObject playerObj;
    public states currentEnemyState;
    public float linearDistance;
    public Transform groundDetector;
    //Private parameters.
    private Animator eAnimator;
    private Rigidbody2D eRb;
    private BoxCollider2D eBxColl;
    private Transform eTransform;
    private bool eIsMovingRight;
    public enum states{IDLE,PATROL,CHASE,DASH,ATTACK}
    // Start is called before the first frame update
    void Start()
    {
        eAnimator = GetComponent<Animator>();
        enemyState = (int)states.IDLE;
        EnemyIdle();
        Invoke("CheckState", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        linearDistance = Vector2.Distance(playerObj.transform.position, this.transform.position);
        CheckState();
        // Locking z axis of the enemy object.
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
        if (enemyHealth<=0)
        {
            Destroy(this);
        }
    }
    void CheckState()
    {
        if (linearDistance >= invokePatrolDist)
        {
            EnemyPatrol();
        }
        else if (linearDistance >= invokeChaseDist && linearDistance <= invokePatrolDist)
        {
            EnemyChase();
        }else if(linearDistance <= invokeChaseDist && linearDistance >= invokeAttackDist)
        {
            EnemyDash();
        }else if (linearDistance <= invokeAttackDist)
        {
            EnemyAttack();
        }        
    }

    public void EnemyAttack() 
    {
        if (linearDistance < invokeAttackDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerObj.transform.position, 10f * Time.deltaTime);
        }
        
        eAnimator.SetBool("isPlayerInRange", true);
        eAnimator.SetBool("canAttack", true);
    }
    

    private void EnemyDash()
    {
        //if player gets closer than 3f, dash to player and turn to attack.
        //if player get back to between 3f and 5f, back to patrol.
        if (linearDistance <= invokeAttackDist && linearDistance >= invokeChaseDist)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, playerObj.transform.position, enemySpeed * 5 * Time.deltaTime);
            //Dash to player, ignore gravity.
            eAnimator.SetBool("isPlayerInrange", true);
            eAnimator.SetBool("canAttack", false);
            eAnimator.SetBool("isChasing", true);
        }
        else
        {
            eAnimator.SetBool("isPlayerInrange", false);
            eAnimator.SetBool("canAttack", true);
            eAnimator.SetBool("isChasing", false);
        }
        //Setting animation by setting booleans.        
    }

    private void EnemyChase()
    {
        //Default State.
        //During patrolling, if the player is closer than 5f but further than 3f, run to the edge of 
        //the platform on the side that the player is at and wait for the player.
        RaycastHit2D hit2D = Physics2D.Raycast(groundDetector.position, Vector2.down, 1f);
        if (linearDistance<= invokeChaseDist)
        {
            if (playerObj.transform.position.x > transform.position.x)
            {
                transform.Translate(Vector2.right * enemySpeed * Time.deltaTime);
                if (hit2D.collider == false || hit2D.collider.CompareTag("Wall"))
                {
                    enemySpeed = 0f;//halt at edge or wall.
                }
            }
            else if (playerObj.transform.position.x < transform.position.x)
            {
                transform.Translate(Vector2.left * enemySpeed * Time.deltaTime);
                if (hit2D.collider == false || hit2D.collider.CompareTag("Wall"))
                {
                    enemySpeed = 0f;//halt at edge or wall.
                }
            }
        }
        //If player leaves the range of 5f, back to Patrol.
        eAnimator.SetBool("isPatrolling", true);
    }

    private void EnemyPatrol()
    {
        transform.Translate(Vector2.right * enemySpeed * Time.deltaTime);
        RaycastHit2D hit2D = Physics2D.Raycast(groundDetector.position, Vector2.down, 1f);
        if (hit2D.collider == false || hit2D.collider.CompareTag("Wall"))
        //turns around when the ray no longer hits anything or hits the wall.
        {
            if (!hit2D.collider.CompareTag("Player"))//won't go back when hits the player.
            {
                if (eIsMovingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    eIsMovingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    eIsMovingRight = true;
                }
            }
        }
        eAnimator.SetBool("isPatrolling", true);
    }
    private void EnemyIdle() 
    {
        enemySpeed = 0;
        transform.position += Vector3.zero;
        //Won't be transitioned to.
        eAnimator.SetBool("isPatrolling", false);
        eAnimator.SetBool("isPlayerInRange", false);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemySpeed = 0;
            enemyHealth -= 1;
            if (enemyHealth <= 0)
            {
                enemySpeed = 0;
                eAnimator.SetTrigger("isDead");
                Destroy(gameObject, 2f);
            }
        }
    }
}
