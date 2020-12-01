using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Controller : MonoBehaviour
{
    //Adjustables
    [SerializeField] int enemyHelath = 3;
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
        enemyState = (int)states.PATROL;        
    }

    // Update is called once per frame
    void Update()
    {
        linearDistance = Vector2.Distance(playerObj.transform.position, this.transform.position);
        CheckState();
    }
    void CheckState()
    {
        switch (currentEnemyState)
        {
            case states.IDLE:
                EnemyIdle();
                break;
            case states.PATROL:
                EnemyPatrol();
                break;
            case states.CHASE:
                EnemyChase();
                break;
            case states.DASH:
                EnemyDash();
                break;
            case states.ATTACK:
                EnemyAttack();
                break;
        }
    }

    private void EnemyAttack() { }

    private void EnemyDash()
    {
        throw new NotImplementedException();
    }

    private void EnemyChase()
    {
        throw new NotImplementedException();
    }

    private void EnemyPatrol()
    {
        transform.Translate(Vector2.right*enemySpeed*Time.deltaTime);
        RaycastHit2D hit2D = Physics2D.Raycast(groundDetector.position, Vector2.down, .1f);
        if (hit2D.collider == false)
        {
            if (eIsMovingRight==true)
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
        eAnimator.SetBool("isPatrolling", true);
    }

    private void EnemyIdle() { }
}
