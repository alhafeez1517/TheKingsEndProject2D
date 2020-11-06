using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTrapController : MonoBehaviour
{
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
            Invoke("DropPlat", .1f);
            Destroy(gameObject, 2f);
        }
    }
    void DropPlat()
    {
        rb.isKinematic = false;
    }
}
