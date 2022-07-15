using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    public float speed;
    public bool enemy = false;
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
        if (!enemy)
        {
            
            if (collision.CompareTag("Enemy"))
            {
                if (collision.GetComponent<Enemy>())
                    collision.GetComponent<Enemy>().TakeDamage();
                if (collision.GetComponent<GunEnemy>())
                    collision.GetComponent<GunEnemy>().TakeDamage();
                if (collision.GetComponent<Boss>())
                    collision.GetComponent<Boss>().TakeDamage();
                Destroy(gameObject);
            }
        }
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player") && enemy)
        {
            if(!collision.GetComponent<PlayerHealth>().immune)
                collision.GetComponent<PlayerHealth>().TakeDamage();
            Destroy(gameObject);
        }
    }
}
