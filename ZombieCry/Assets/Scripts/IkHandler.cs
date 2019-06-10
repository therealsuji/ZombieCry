using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkHandler : MonoBehaviour
{
    protected Animator animator;
    public bool ikActive = false;
    new Camera camera;
    public Vector3 lookAtPosition;
    public Vector3 lookAtPositionOffest;

    void Start()
    {
        camera = GameObject.FindObjectOfType<Camera>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        lookAtPosition = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10)) + lookAtPositionOffest;


    }
    void OnAnimatorIK()
    {


        if (animator)
        {

            if (ikActive)
            {

                animator.SetLookAtWeight(1, 1);
                animator.SetLookAtPosition(lookAtPosition);

            }

            else
            {
                animator.SetLookAtWeight(0);
            }
        }
    }
}
