using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{ 

    public Light valo;
    public Transform cam;
    public Transform zombie;

    PlayerInputActions pia;
    CharacterController player;

    public float normalSpeed = 10;
    public float fastSpeed = 30;
    float currentSpeed = 10;

    Vector3 gVelocity;
    public float gravity = -10f;
    public Transform bottom;
    public LayerMask groundMask;
    bool grounded;

    Transform pickupItem;

    // Start is called before the first frame update
    void Start()
    {
        gVelocity = Vector3.zero;
        pia = new PlayerInputActions();
        pia.Enable();

        pia.Land.Pick.started += (x) => Pickup();
        pia.Land.Pick.canceled += (x) => Drop();



     
        player = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        ControlMove();
        ControlGravity();
        ControlJump();

     
        /*Koodi oven avaukseen raycastilla (katsesuunta)
         
        RaycastHit rHit;
        bool collide = Physics.Raycast(transform.position, transform.forward, out rHit, 5);

        Debug.DrawRay(transform.position, transform.forward * 10, Color.red);

        if(collide && rHit.collider.tag == "Door")
        {
            rHit.collider.GetComponent<Animator>().SetTrigger("OpenDoor");
        }*/
        
    }

    void Pickup()
    {
        Transform cam = Camera.main.transform;

        RaycastHit hit;

        //L‰hetet‰‰n laatikkomallinen "s‰de" kameran keskelt‰
        //et‰isyydelle 3. Regoidaan vain collidereihin, joilla Layer on Pickable
        bool isHit = Physics.BoxCast(transform.position, new Vector3(1, 1, 1), 
            transform.forward, out hit , transform.rotation, 3, LayerMask.GetMask("Pickable"));

        if(isHit)
        {
            pickupItem = hit.transform;
            pickupItem.GetComponent<Rigidbody>().isKinematic = true;
            pickupItem.parent = transform;
            pickupItem.localPosition = new Vector3(0, 0, 1.5f);

            pickupItem.GetComponent<Renderer>().material.color = Color.yellow;
        }

    }

    void Drop()
    {
        if( pickupItem != null )
        {
            pickupItem.GetComponent<Rigidbody>().isKinematic = false;
            pickupItem.parent = null;

            pickupItem.GetComponent<Renderer>().material.color = Color.white;

            pickupItem = null;
        }
    }


    void ControlJump()
    {
        if( pia.Land.Jump.triggered && grounded)
        {
            gVelocity.y = 5f;
        }
    }

    void ControlGravity()
    {
        grounded = Physics.CheckSphere(bottom.position, 0.4f, groundMask);

        if( grounded && gVelocity.y < 0)
        {
            gVelocity.y = -2f;
        }

        gVelocity.y += gravity * Time.deltaTime;

        player.Move(gVelocity * Time.deltaTime);

    }

    //K‰‰nnyt‰‰n aina kameran katsesuuntaan ja liikutaan vain eteen/taakse
    /*void ControlMove()
    {
        //Valitaan nopeus (shift-nappi)
        currentSpeed = pia.Land.Fast.ReadValue<float>() > 0 ? fastSpeed : normalSpeed;
        
        //Haetaan nuolin‰pp‰inten arvot vektorina
        Vector2 move = pia.Land.Move.ReadValue<Vector2>();

        //Haetaan kameran nokkasuunta
        Vector3 camDir = cam.forward;
        //Kameran vertikaalisuuntaan ei tarvita
        camDir.y = 0; 
        
        //K‰‰nnyt‰‰n aina kameran katsesuuntaan.
        transform.rotation = Quaternion.LookRotation(camDir);

        //Liikutetaan nuolin‰pp‰inten mukaiseen suuntaan
        Vector3 moveDirection = transform.forward * move.y;// + transform.right * move.x;
        player.Move(moveDirection.normalized * Time.deltaTime * currentSpeed);

        //M‰‰ritell‰‰n animattion nopeus (vaikuttaa myˆs animaation siirtym‰‰n idle-->run)
        float speed = moveDirection.normalized.magnitude * currentSpeed / 10;
        zombie.GetComponent<Animator>().SetFloat("MoveSpeed", speed);

    }*/

    void ControlMove()
    {
        //Valitaan nopeus (shift-nappi)
        currentSpeed = pia.Land.Fast.ReadValue<float>() > 0 ? fastSpeed : normalSpeed;

        //Haetaan nuolin‰pp‰inten arvot vektorina
        Vector2 move = pia.Land.Move.ReadValue<Vector2>();

        //Liikesuunta kameran myˆt‰iseksi (nuoli ylˆs = kameran katsesuuntaan liike
        //nuoli oikea = kameran oikealle liike). Tehd‰‰n n‰ist‰ vektorisuunta.
        Vector3 moveDirection = cam.forward * move.y + cam.right * move.x;
        moveDirection.y = 0;

        if (moveDirection.magnitude > 0)
        {
            //transform.rotation = Quaternion.RotateTowards(transform.rotation,
              //  Quaternion.LookRotation(moveDirection), Time.deltaTime * 300);

            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(moveDirection), 0.05f);

            player.Move(moveDirection.normalized * Time.deltaTime * currentSpeed);
        }


        float speed = moveDirection.normalized.magnitude * currentSpeed / 10;
        //Juoksuanimaation liike aiheuttaa ongelmaa, jossa zombie siirtyy character controllerin keskelt‰ pois. 
        zombie.GetComponent<Animator>().SetFloat("MoveSpeed", speed);



    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Box")
        {
         
            hit.gameObject.GetComponent<Animator>().Play("OpenBox");
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "AmmoBox")
        {
            //Pelaajan toisella scriptill‰ ShootControl on metodi MoreAmmo
            gameObject.SendMessage("MoreAmmo", 10);
        }
        else if( other.tag == "Door" )
        {
            other.GetComponent<Animator>().SetTrigger("OpenDoor");
        }
    }

}
