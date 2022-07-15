using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemy : MonoBehaviour
{
    public int maxhp = 7;
    public float currenthp;
    public AudioClip destroysound;
    public float detectRange = 5f;
    public bool detected = false;
    public Transform target;
    public float speed = 2f;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    public float maxCD=2f;
    public float currentCD=0;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        currenthp = maxhp;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currenthp <= 0)
        {
            SoundManager.Instance.PlayClip(destroysound);
            Destroy(gameObject);
        }
        DetectPlayer();
        currentCD -= Time.deltaTime;
        currentCD = Mathf.Max(currentCD, 0);
    }
    public void DetectPlayer()
    {
        int layerMask = 1 << 8;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * transform.localScale.x, detectRange, layerMask);
        // Does the ray intersect any objects excluding the player layer
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, transform.right * hit.distance, Color.yellow);
            if (hit.collider.CompareTag("Player"))
            {
                Shoot();
            }

        }
    }
    public void TakeDamage()
    {
        StartCoroutine(Red());
        currenthp -= 1;
    }
    IEnumerator Red()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        sr.color = Color.white;
    }
    public void Shoot()
    {
        if (currentCD <= 0)
        {
            currentCD = maxCD;
            GameObject bul = Instantiate(bullet);
            bul.transform.position = transform.position;
            if(transform.localScale.x==-1)
                bul.GetComponent<SpriteRenderer>().flipX = true;
            if (transform.localScale.x == 1)
                bul.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
