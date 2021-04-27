using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeExplosion : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    void Explosion()
    {
        float xForce;
        float yForce;
        float zForce;

       
        foreach (Transform child in transform)
        {
            xForce = Random.Range(-100, 100);
            yForce = Random.Range(0, 100);
            zForce = Random.Range(-100, 100);

            child.GetComponent<Rigidbody>().isKinematic = false;

            child.GetComponent<Rigidbody>().AddForce(xForce, yForce, zForce);

        }

        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    
   
}
