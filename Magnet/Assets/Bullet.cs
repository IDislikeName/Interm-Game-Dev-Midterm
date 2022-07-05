using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (sr.flipX)
        {
            rb.velocity = -transform.right * speed;
        }
        else
        {
            rb.velocity = transform.right * speed;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
