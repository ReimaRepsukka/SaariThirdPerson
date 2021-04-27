using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 suunta = player.position - transform.position;
        

        Quaternion lookRot = Quaternion.LookRotation(suunta, Vector3.up);

        //transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRot, Time.deltaTime*40);

        if ( suunta.magnitude > 4 )
            transform.Translate(transform.forward * Time.deltaTime * 3, Space.World);

       
       

    }
}
