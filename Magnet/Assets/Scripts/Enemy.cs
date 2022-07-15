using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxhp=7;
    public float currenthp;
    public AudioClip destroysound;
    public float detectRange = 3f;
    public bool detected = false;
    public Transform target;
    public float speed = 2f;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    public Sprite angy;
    public Sprite notAngy;

    public Transform[] waypoints;
    public int currentIndex;
    private bool adding;
    // Start is called before the first frame update
    void Start()
    {
        currenthp = maxhp;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        currentIndex = 0; 
        target = waypoints[currentIndex];
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
        if (target!=null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if (Mathf.Abs(transform.position.x - target.position.x) <= 0.2f&&!target.CompareTag("Player")&&!adding)
            {
                StartCoroutine(nextIndex());
            }
            if (target.CompareTag("Player"))
            {
                if(transform.position.x - target.position.x >= 5f)
                {
                    sr.sprite = notAngy;
                    target = waypoints[currentIndex];
                }
            }
            if (target.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);

            }
            else if(target.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
    public void DetectPlayer()
    {
        int layerMask = 1 << 8;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right*transform.localScale.x, detectRange, layerMask);
        // Does the ray intersect any objects excluding the player layer
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, transform.right * hit.distance, Color.yellow);
            if (hit.collider.CompareTag("Player"))
            {
                sr.sprite = angy;
                target = hit.collider.transform;
            }

        }
    }
    public void TakeDamage()
    {
        StartCoroutine(Red());
        currenthp -= 1;
    }
    IEnumerator nextIndex()
    {
        adding = true;
        currentIndex++;
        
        if (currentIndex >= waypoints.Length)
        {
            currentIndex = 0;
        }
        yield return new WaitForSeconds(0.4f);
        target = waypoints[currentIndex];
        adding = false;

    }
    IEnumerator Red()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        sr.color = Color.white;
    }
}
