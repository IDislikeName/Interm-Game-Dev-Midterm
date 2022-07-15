using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject boss;
    public GameObject door;
    private bool started = false;
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
        if (collision.CompareTag("Player")&&!started)
        {
            started = true;
            boss.GetComponent<Boss>().activated = true;
            StartCoroutine(Music());
            door.SetActive(true);            
        }
        
    }
    IEnumerator Music()
    {
        SoundManager.Instance.bgm.Stop();
        yield return new WaitForSeconds(3f);
        SoundManager.Instance.PlayBGM(SoundManager.Instance.bossmusic);
        Destroy(gameObject);
    }
}
