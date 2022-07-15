using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockGun : MonoBehaviour
{
    public GameObject unlockgun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerGuns>().guns.Add(unlockgun);
            collision.GetComponent<PlayerGuns>().currentWeapon = unlockgun;
            unlockgun.SetActive(true);
            collision.GetComponent<PlayerGuns>().currentindex = collision.GetComponent<PlayerGuns>().guns.Count - 1;
            Destroy(gameObject);
        }
    }
}
