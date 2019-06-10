using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    new Camera camera;
    Animator animator;
    CharacterController charachterController;
    public bool cursorLocked;
    private Vector3 moveDirection = Vector3.zero;
    public float speed;
    public float inputX;
    public float inputY;
    float jumpForce = 15;
    float verticalVelocity;
    private float gravityJump = 14.0f;
    public float gravity = -9.8f;
    Transform bulletSpawnPoint;
    Vector3 hitPoint;
    void Start()
    {
        lockCursor();
        animator = GetComponent<Animator>();
        camera = GameObject.FindObjectOfType<Camera>();
        charachterController = GetComponent<CharacterController>();
    }

    void GetAimPoint()
    {
        Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Debug.DrawRay(bulletSpawnPoint.transform.position, camera.transform.forward * 100, Color.green);
        Debug.DrawRay(camera.transform.position, camera.transform.forward * 100, Color.green);

        if (Physics.Raycast(rayOrigin, camera.transform.forward, out hit, 50))
        {
            hitPoint = hit.point;
        }
        else
        {
            hitPoint = camera.transform.forward * 50;
        }

    }

    void FixedUpdate()
    {
        GetAimPoint();
        if (Input.GetMouseButtonDown(0))
        {

        }
        else
        {
        }
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");



        //run animations
        animator.SetFloat("moveForward", inputY);

        animator.SetFloat("moveRight", inputX);

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
                animator.SetBool("jumping", true);
            }
        }
        else
        {
            verticalVelocity -= gravityJump * Time.deltaTime;
            animator.SetBool("jumping", false);

        }

        Vector3 jumpVector = new Vector3(0, verticalVelocity, 0);
        charachterController.Move(jumpVector * Time.deltaTime);
    }


    void lockCursor()
    {
        if (cursorLocked)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

}
