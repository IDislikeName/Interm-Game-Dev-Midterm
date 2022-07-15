using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIstuff : MonoBehaviour
{
    public PlayerGuns pg;
    public TMP_Text ammoText;
    public Image weaponIMG;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pg.guns.Count != 0)
        {
            ammoText.text = pg.currentWeapon.GetComponent<Gun>().currentAmmo + "/" + pg.currentWeapon.GetComponent<Gun>().maxAmmo;
            weaponIMG.sprite = pg.currentWeapon.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            ammoText.text = "";
        }
        
    }
}
