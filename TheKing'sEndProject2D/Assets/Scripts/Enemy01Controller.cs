using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Controller : MonoBehaviour
{
    //Adjustables
    [SerializeField] int enemyHelath = 3;
    [SerializeField] int enemyCurrentHealth;
    [SerializeField] float enemySpeed = 3f;
    [SerializeField] float invokeIdleDist = 10f;
    [SerializeField] float invokeChaseDist = 5f;
    [SerializeField] float invokeAttackDist = 3f;
    //Public parameters.
    public int enemyState;
    //Private parameters.
    private Animator eAnimator;
    private Rigidbody2D eRb;
    private BoxCollider2D eBxColl;
    private Transform eTransform;
    private enum states{IDLE,PATROL,CHASE,DASH,ATTACK}
    // Start is called before the first frame update
    void Start()
    {
        enemyState = (int)states.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
    }
    void CheckState()
    {
        switch (states)
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
}
