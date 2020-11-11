using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTrapController : MonoBehaviour
{
    [SerializeField] float timeBeforeDrop = 1f;
    [SerializeField] float timeToDestroy = 2f;
    [SerializeField] bool destroyOnContact = false;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Invoke("DropPlat", timeBeforeDrop);
        }

        if (destroyOnContact == true)
        {
            if (collision.gameObject.tag.Equals("Ground") || collision.gameObject.tag.Equals("Trap") || collision.gameObject.tag.Equals("Wall"))
            {
                Destroy(gameObject);
            }
        }
    }
    void DropPlat()
    {
        //GetComponent<Collider2D>().enabled = false;
        rb.isKinematic = false;
        Destroy(gameObject, timeToDestroy);
    }
}
