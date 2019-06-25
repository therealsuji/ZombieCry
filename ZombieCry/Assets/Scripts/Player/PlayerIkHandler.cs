using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIkHandler : MonoBehaviour
{
    protected Animator animator;
    public bool ikActive;
    public bool lookIk;
    public bool handIk;
    new Camera camera;
    Vector3 lookAtPosition;
    public Vector3 lookAtPositionOffest;
    public Transform leftHand;
    public Transform rightHand;
    public GameObject weapon;
    public float lookWeight, bodyWeight, headWeight, eyesWeight, clampWeight;
    void Start()
    {
        camera = GameManager.instance.camera;
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
                //weapon.transform.LookAt(lookAtPosition);
                animator.SetLookAtWeight(lookWeight, bodyWeight, headWeight, eyesWeight, clampWeight);
                if (handIk)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.5f);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.5f);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
                }
                if (lookIk)
                {
                    animator.SetLookAtPosition(lookAtPosition);

                }



            }

            else
            {
                animator.SetLookAtWeight(0);
            }
        }
    }
}
