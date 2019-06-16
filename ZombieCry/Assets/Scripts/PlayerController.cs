using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    new Camera camera;
    CharacterController charachterController;
    Transform bulletSpawnPoint;
    public GameObject bullet;

    public bool cursorLocked;
    public float speed;
    public float inputX;
    public float inputY;
    public float jumpForce = 15;
    float verticalVelocity;
    private float gravityJump = 14.0f;
    public float gravity = -9.8f;
    public float bulletSpeed;
    Vector3 hitPoint;

    RagDollHandler playerRagDollHandler;
    void Start()
    {
        playerRagDollHandler = GetComponent<RagDollHandler>();
        animator = GetComponent<Animator>();
        camera = GameManager.instance.camera;
        charachterController = GetComponent<CharacterController>();
        bulletSpawnPoint = GameObject.FindGameObjectWithTag("weaponEnd").GetComponent<Transform>();
    }

    void GetAimPoint()
    {
        Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Debug.DrawRay(bulletSpawnPoint.transform.position, camera.transform.forward * 50, Color.green);
        Debug.DrawRay(camera.transform.position, camera.transform.forward * 50, Color.green);

        if (Physics.Raycast(rayOrigin, camera.transform.forward, out hit, 50))
        {
            hitPoint = hit.point;
        }
        else
        {
            hitPoint = bulletSpawnPoint.position + camera.transform.forward * 100;
        }

    }

    void FixedUpdate()
    {

        lockCursor();

        if (GameManager.instance.playerIsDead)
        {
            playerRagDollHandler.Die();
        }
        else
        {
            GetAimPoint();
            if (Input.GetMouseButtonDown(0))
            {
                GameObject bulletObj = Instantiate(bullet);
                bulletObj.transform.position = bulletSpawnPoint.position;
                bulletObj.GetComponent<Rigidbody>().AddForce((hitPoint - bulletObj.transform.position).normalized * bulletSpeed, ForceMode.Impulse);
            }
            else
            {
            }

            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");


            //run animations
            animator.SetFloat("moveForward", inputY, 1f, Time.deltaTime * 10f);

            animator.SetFloat("moveRight", inputX, 1f, Time.deltaTime * 10f);

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
