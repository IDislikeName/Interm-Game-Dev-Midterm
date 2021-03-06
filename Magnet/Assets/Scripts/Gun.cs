

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectile;
    public float projOffset;
    public PlayerMovement mvmt;

    public float weight;
    public int maxAmmo;
    public int currentAmmo;
    public float reloadTime;

    public float shotCD;
    public float currentCD;
    public float recoilF;
    public SpriteRenderer sr;

    public bool reloading = false;

    public AudioClip shootsound;
    public AudioClip reloadsound;
    // Start is called before the first frame update
    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        currentAmmo = maxAmmo;
        mvmt = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetMouseButton(0) && currentCD <= 0 && currentAmmo > 0&&!reloading)
        {
            Shoot();
        }
        currentCD -= Time.deltaTime;
        currentCD = Mathf.Max(currentCD, 0);
        Vector3 v3 = Input.mousePosition;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        float rot = 0;
        if (v3.x >= transform.position.x)
        {
            rot = Mathf.Atan2(v3.y - transform.position.y, v3.x - transform.position.x) * Mathf.Rad2Deg;
            sr.flipX = false;
        }
        else
        {
            rot = Mathf.Atan2(v3.y - transform.position.y, v3.x - transform.position.x) * Mathf.Rad2Deg - 180;
            sr.flipX = true;
        }
        transform.rotation = Quaternion.Euler(0, 0, rot);

        if (Input.GetKeyDown(KeyCode.R)||currentAmmo==0)
        {
            if(!reloading&&currentAmmo<maxAmmo)
                Reload();
        }
    }
    public void Shoot()
    {
        currentAmmo--;
        currentCD = shotCD;
        SoundManager.Instance.PlayClip(shootsound);
        GameObject bullet = Instantiate(projectile);
        if (sr.flipX)
        {
            bullet.transform.position = transform.position - transform.right * projOffset;
            bullet.transform.rotation = transform.rotation;
        }
        else
        {
            bullet.transform.position = transform.position + transform.right * projOffset;
            bullet.transform.rotation = transform.rotation;
        }
        bullet.GetComponent<SpriteRenderer>().flipX = sr.flipX;
        if (sr.flipX)
        {
            GetComponentInParent<Rigidbody2D>().AddForce(transform.right * recoilF);
        }
        else
        {
            GetComponentInParent<Rigidbody2D>().AddForce(-transform.right * recoilF);
        }
    }
    public void Reload()
    {
        if(!reloading)
            StartCoroutine(Re());
    }
    IEnumerator Re()
    {
        reloading = true;
        SoundManager.Instance.PlayClip(reloadsound);
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        reloading = false;
    }
}
