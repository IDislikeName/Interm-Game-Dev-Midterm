using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health = 50;
    public bool activated = false;
    private Rigidbody2D rb;
    public GameObject projectile;
    private SpriteRenderer sr;
    public AudioClip destroysound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(BossCycle());
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x > 0)
        {
            sr.flipX = false;
        }
        else if(rb.velocity.x < 0)
        {
            sr.flipX = true;
        }
        if (health <= 0)
        {
            SoundManager.Instance.PlayClip(destroysound);
            GameManager.Instance.victory.SetActive(true);
            SoundManager.Instance.bgm.Stop();
            Destroy(gameObject);
        }
    }
    IEnumerator BossCycle()
    {
        yield return new WaitUntil(()=>activated==true);
        yield return new WaitForSeconds(3f);
        rb.velocity = new Vector2(6, 0);
        yield return new WaitForSeconds(1f);
        rb.velocity = new Vector2(-6, 0);
        yield return new WaitForSeconds(2f);
        rb.velocity = new Vector2(6, 0);
        yield return new WaitForSeconds(2f);
        rb.velocity = new Vector2(-6, 0);
        yield return new WaitForSeconds(1f);
        rb.velocity = new Vector2(0, 0);
        Shoot();
        yield return new WaitForSeconds(1.5f);
        Shoot();
        yield return new WaitForSeconds(1.5f);
        Shoot();
        yield return new WaitForSeconds(1.5f);
        Shoot();
        yield return new WaitForSeconds(3f);
        Jump();
        yield return new WaitForSeconds(2f);
        rb.velocity = new Vector2(0, rb.velocity.y);
        Jump();
        yield return new WaitForSeconds(2f);
        rb.velocity = new Vector2(0, rb.velocity.y);
        Jump();
        yield return new WaitForSeconds(2f);
        rb.velocity = new Vector2(0, rb.velocity.y);
        yield return new WaitForSeconds(1.5f);
        Shoot();
        yield return new WaitForSeconds(1.5f);
        Shoot();
        yield return new WaitForSeconds(1.5f);
        Shoot();
        StartCoroutine(BossCycle());

    }
    public void Shoot()
    {
        GameObject porj = Instantiate(projectile);
        porj.transform.position = transform.position;
        porj.transform.right = GameManager.Instance.Player.transform.position - transform.position;
        porj.GetComponent<Rigidbody2D>().velocity = porj.transform.right * 4f;
    }
    public void Jump()
    {
        if (GameManager.Instance.Player.transform.position.x > transform.position.x)
        {
            rb.velocity = new Vector2(4, 8);
        }
        else
        {
            rb.velocity = new Vector2(-4, 8);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!collision.gameObject.GetComponent<PlayerHealth>().immune)
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage();
        }
    }
    public void TakeDamage()
    {
        StartCoroutine(Red());
        health -= 1;
    }
    IEnumerator Red()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        sr.color = Color.white;
    }
}
