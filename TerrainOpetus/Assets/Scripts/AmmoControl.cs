using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyAmmo", 3f);
    }

    void DestroyAmmo()
    {
        Destroy(gameObject);
    }

}
