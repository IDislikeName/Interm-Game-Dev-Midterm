using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool immune = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage()
    {
        StartCoroutine(Damage());
    }
    IEnumerator Damage()
    {
        GameManager.Instance.currentHealth -= 1;
        GetComponent<SpriteRenderer>().color = Color.red;
        immune = true;
        yield return new WaitForSeconds(0.3f);
        immune = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
