using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerRoot;

    PlayerInputActions pia;

    float currenRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        pia = new PlayerInputActions();
        pia.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseLook = pia.Land.View.ReadValue<Vector2>() * Time.deltaTime * 10;

        playerRoot.Rotate(0, mouseLook.x, 0);

        currenRotation -= mouseLook.y;
        currenRotation = Mathf.Clamp(currenRotation, -90, 90);
        transform.localRotation = Quaternion.Euler(currenRotation, 0, 0);

      

        //transform.Rotate(-mouseLook.y, 0, 0);

   
    }
}
