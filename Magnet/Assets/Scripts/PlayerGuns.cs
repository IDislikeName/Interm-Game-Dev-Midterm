using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuns : MonoBehaviour
{
    public List<GameObject> guns;
    public GameObject currentWeapon;
    public int currentindex = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (guns.Count != 0)
        {
            currentWeapon = guns[0];
            currentindex = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)&&!currentWeapon.GetComponent<Gun>().reloading)
        {
            if (guns.Count != 0)
                ChangeWeapons();
        }
    }
    public void ChangeWeapons()
    {
        currentWeapon.SetActive(false);
        currentindex++;
        if (currentindex > guns.Count - 1)
        {
            currentindex = 0;
        }
        guns[currentindex].SetActive(true);
        currentWeapon = guns[currentindex];
    }
}
