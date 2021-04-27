using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControlMy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Suorita", 5f);
    }

    void Suorita()
    {
        GetComponent<Animator>().Play("OpenDoor");
    }
}
