using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCountScript : MonoBehaviour
{
    public Text maxAmmoTxt;
    public Text currentAmmoTxt;


    private void Start()
    {
        maxAmmoTxt.text = GetComponent<GunScript>().magSize.ToString();
    }

    private void Update()
    {
        currentAmmoTxt.text = GetComponent<GunScript>().bulletsInMag.ToString();
    }
}
