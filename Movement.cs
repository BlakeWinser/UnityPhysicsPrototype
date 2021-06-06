using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool canMove = true;

    public Camera playerCam;
    public Transform player;
    CharacterController playerController;

    public float walkSpeed = 7.5f;
    public float runSpeed = 11.5f;
    public float jumpHeight = 8f;
    public float gravity = 20f;

    public float jetpackFuel = 100f;
    public float maxJetpackFuel = 100f;

    public float maxEnergy = 100f;
    public float currentEnergy = 100f;

    public EnergyBar energyBar;

    public float explosionForce = 1500f;
    public float explosionRadius = 10f;
 
    Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        playerController = GetComponent<CharacterController>();
        energyBar.SetMaxEnergy(maxJetpackFuel);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        //Walking and Running
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxisRaw("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxisRaw("Horizontal") : 0;
        
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        //Jump
        if (Input.GetButton("Jump") && canMove && playerController.isGrounded)
        {
            moveDirection.y = jumpHeight;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        //Jetpack
        if (Input.GetKey(KeyCode.V) && canMove && !playerController.isGrounded && jetpackFuel > 0)
        {
            moveDirection.y = movementDirectionY;
            moveDirection.y += 40 * Time.deltaTime;

            jetpackFuel -= 70 * Time.deltaTime;

            energyBar.setEnergy(jetpackFuel);

            Debug.Log(jetpackFuel);
        }

        if (!Input.GetKey(KeyCode.V) && jetpackFuel < maxJetpackFuel)
        {
            jetpackFuel += 20 * Time.deltaTime;

            energyBar.setEnergy(jetpackFuel);
        }

        //Gravity
        if (!playerController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        //Apply all movement
        playerController.Move(moveDirection * Time.deltaTime);
    }

    void Crouch()
    {
        //To-Do
    }

}