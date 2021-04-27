using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootControl : MonoBehaviour
{

    PlayerInputActions pia;

    public Text bulletNumText;
    public GameObject ammoPrefab;
    public float forceMultiplier = 100;

    int bullets = 10;

    // Start is called before the first frame update
    void Start()
    {
        pia = new PlayerInputActions();
        pia.Enable();

        RefreshBulletCount();
    }

    // Update is called once per frame
    void Update()
    {
        //Onko painettu shoot-nappia
        if (pia.Land.Shoot.triggered && bullets > 0)
        {
            //Haetaan ampumiseen sijainti pelaajan läheltä
            Vector3 shootPos = transform.position
                + transform.forward * 0.5f;


            //Luodaan ammus valittuun kohtaan.
            GameObject ammo = Instantiate(ammoPrefab, shootPos, Quaternion.identity);

            //Asetetaan ammukselle lähtövoima
            ammo.GetComponent<Rigidbody>().AddForce(transform.forward * forceMultiplier);
           
            //Päivitetään ammusten määrä
            bullets--;
            RefreshBulletCount();
        }
    }

    void RefreshBulletCount()
    {
        if(bulletNumText != null)
            bulletNumText.text = bullets.ToString();
    }

    void MoreAmmo(int ammos)
    {
        bullets += ammos;
        RefreshBulletCount();  
    }

}
