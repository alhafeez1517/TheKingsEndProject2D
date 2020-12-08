using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinAttackBlock : MonoBehaviour
{
    private AssassinController assassinController;
    private bool isAttacking = false;
    [SerializeField] bool isBlocking = false;
    //private Enemy01Controller enemy01Controller;

    void Start()
    {
        assassinController = GameObject.FindGameObjectWithTag("Player").GetComponent<AssassinController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            assassinController.Slash();
            Debug.Log("Attack");
            // Code for enemy public method            
            // ???.TakeDamage();
        }        

        if (isBlocking == true && collision.tag == "Attack")
        {
            Debug.Log("Block");
            assassinController.BlockAnimation();
        }        
                
        if (collision.tag == "Arrow")
        {
            Destroy(collision.gameObject);
        }
    }

    public void SetIsBlocking(bool isBlock)
    {
        //Debug.Log(isBlock);
        isBlocking = isBlock;
    }
}
