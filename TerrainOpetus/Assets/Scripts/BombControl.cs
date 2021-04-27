using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombControl : MonoBehaviour
{
    public ParticleSystem explosionPrefab;

    private ParticleSystem currentExplosion;

    private void Start()
    {
        //R‰j‰ytet‰‰n pommi 5sek p‰‰st‰ luomisen j‰lkeen
        Invoke("Explode", 5f);   
    }

    void Explode()
    {
        //Luodaan s‰teell‰ 5 pallo, johon tˆrm‰‰v‰t colliderit haetaan.
        Collider[] hits = Physics.OverlapSphere(transform.position, 5);

        //K‰yd‰‰n colliderit l‰pi. Jos colliderilla on rigidbody, muutetaan se ei-kinemaattiseksi
        //ja aiheutetaan sille r‰j‰hdysvoima.
        foreach(Collider c  in hits)
        {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                //R‰j‰hdysen voima 50, s‰de 5 ja ylˆsp‰in suuntautuva kerroin 10
                rb.AddExplosionForce(200f, transform.position, 5, 100f);
            }
        }

        //Luodaan pommin kohdalle r‰j‰dyksen partikkelisysteemi ja laitetaan partikkelit p‰‰lle
        currentExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        currentExplosion.Play();

        //Piilotetaan pommi heti r‰j‰hdysess‰
        gameObject.SetActive(false);

        //Kutsutaan tuhometodia 3sek p‰‰st‰
        //T‰m‰ tuhoaa viiveell‰ pommin ja partikkelit, jotta partikkelit ehtiv‰t n‰ky‰ kokonaan,
        //mutteiv‰t j‰‰ peliin turhina objekteina.
        Invoke("DestroyBomb", 3f);
    }


    //T‰m‰ metodi tuhoaa sek‰ pommin ett‰ partikkelisysteemin pelist‰.
    void DestroyBomb()
    {
        Destroy(currentExplosion.gameObject);
        Destroy(gameObject);
    }
}
