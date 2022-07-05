using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);


    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Attract();
        }
    }
    public void Attract()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 5f, layerMask);
        // Does the ray intersect any objects excluding the player layer
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, transform.right * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            if (hit.collider.CompareTag("Metalobj"))
            {
                hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.right*10f);
            }

        }
        else
        {
            Debug.DrawRay(transform.position, transform.right * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
