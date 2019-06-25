using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorHelper : MonoBehaviour
{
    Animator playerAnim;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    public void Move(bool state)
    {
        playerAnim.SetBool("move", state);

    }

    public void MeleeMode()
    {
        playerAnim.SetInteger("mode", (int)WeaponTypeEnum.WeaponType.Melee);

    }
    public void GunMode()
    {
        playerAnim.SetInteger("mode", (int)WeaponTypeEnum.WeaponType.Gun);

    }
    public void OneHandMelee(bool state)
    {
        playerAnim.SetInteger("mode", (int)WeaponTypeEnum.WeaponType.OneHanded);

    }
    public void TwoHandMelee(bool state)
    {
        playerAnim.SetInteger("mode", (int)WeaponTypeEnum.WeaponType.TwoHanded);

    }
    public void MoveForward(float inputY)
    {
        playerAnim.SetFloat("moveForward", inputY, 1f, Time.deltaTime * 10f);

    }
    public void MoveRight(float inputY)
    {
        playerAnim.SetFloat("moveRight", inputY, 1f, Time.deltaTime * 10f);

    }
    public void Jump(bool state)
    {
        playerAnim.SetBool("isJumping", state);

    }
    public void PlayAttack(string attackString)
    {
        playerAnim.SetBool("isPunching", true);
        playerAnim.CrossFade(attackString, 0.2f);
        playerAnim.SetBool("isPunching", false);

    }

    void Update()
    {
        playerAnim.applyRootMotion = playerAnim.GetBool("isAttacking");
    }
}
