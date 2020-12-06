using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTrapController : MonoBehaviour
{
    [SerializeField] float timeBeforeDrop = 0.5f;
    [SerializeField] float delayTime = 5f;
    [SerializeField] bool isReturning = true;

    private float originalPositionY;
    private Rigidbody2D rb;
    private bool changeToTransparent = false;

    // Start is called before the first frame update
    void Start()
    {
        originalPositionY = transform.position.y;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Invoke("DropPlat", timeBeforeDrop);
        }

        //if (isReturning == false)
        //{
        //    if (collision.gameObject.tag.Equals("Ground") || collision.gameObject.tag.Equals("Trap") || collision.gameObject.tag.Equals("Wall"))
        //    {
        //        Destroy(gameObject);
        //    }
        //}
    }
    void DropPlat()
    {
        GetComponent<Collider2D>().enabled = false;
        rb.isKinematic = false;   

        if (isReturning == true)
        {
            Invoke("Return", delayTime);
        }
        else if (isReturning == false)
        {
            Invoke("DestroyPlatform", delayTime);
        }
    }

    void Return()
    {
        GetComponent<Collider2D>().enabled = true;
        rb.bodyType = RigidbodyType2D.Static;
        transform.position = new Vector2(transform.position.x, originalPositionY);
    }

    void DestroyPlatform()
    {
        Destroy(gameObject);
    }
}
