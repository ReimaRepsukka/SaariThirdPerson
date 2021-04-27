using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxControl : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
