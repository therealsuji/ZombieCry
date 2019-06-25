using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    new Camera camera;
    CharacterController charachterController;

    public bool cursorLocked;
    public float speed;
    public float inputX;
    public float inputY;
    public float jumpForce = 15;
    float verticalVelocity;
    private float gravityJump = 14.0f;
    public float gravity = -9.8f;

    RagDollHandler playerRagDollHandler;
    PlayerAnimatorHelper animatorHelper;
    void Start()
    {
        animatorHelper = GetComponent<PlayerAnimatorHelper>();
        playerRagDollHandler = GetComponent<RagDollHandler>();
        camera = GameManager.instance.camera;
        charachterController = GetComponent<CharacterController>();

    }


    void FixedUpdate()
    {
        //lock cursor 
        LockCursor();
        //check if player is dead
        if (GameManager.instance.playerIsDead)
        {
            playerRagDollHandler.Die();
        }
        else
        {
            MovementInputHandler();
            MovementHandler();


        }
    }


    void LockCursor()
    {
        if (cursorLocked)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

    void MovementInputHandler()
    {
        //movement input
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
    }

    void MovementHandler()
    {
        if (inputX > 0 || inputX < 0 || inputY > 0 || inputY < 0)
        {
            animatorHelper.Move(true);
        }
        else
        {
            animatorHelper.Move(false);
        }
        //run animations
        animatorHelper.MoveForward(inputY);
        animatorHelper.MoveRight(inputX);

        //Rotate the player according to camera
        var characterRotation = camera.transform.rotation;
        characterRotation.x = 0;
        characterRotation.z = 0;
        transform.rotation = characterRotation;

        //movement
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);

        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        charachterController.Move(movement);


        if (charachterController.isGrounded)
        {
            verticalVelocity = -gravityJump * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
                animatorHelper.Jump(true);
            }
        }
        else
        {
            print("falling");
            verticalVelocity -= gravityJump * Time.deltaTime;
            animatorHelper.Jump(false);
        }

        Vector3 jumpVector = new Vector3(0, verticalVelocity, 0);
        charachterController.Move(jumpVector * Time.deltaTime);
    }



}
