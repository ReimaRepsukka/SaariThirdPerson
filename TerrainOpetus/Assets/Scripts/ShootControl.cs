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
            //Haetaan ampumiseen sijainti kameran edestä
            Vector3 shootPos = Camera.main.transform.position
                + Camera.main.transform.forward * 0.5f;

            //Aampumissuunta kameran forward suuntaan
            Vector3 shootDirection = Camera.main.transform.forward;

            //Luodaan ammus valittuun kohtaan.
            GameObject ammo = Instantiate(ammoPrefab, shootPos, Quaternion.identity);

            //Asetetaan ammukselle lähtövoima
            ammo.GetComponent<Rigidbody>().AddForce(shootDirection * forceMultiplier);
           
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
