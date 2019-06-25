using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    new Camera camera;
    Vector3 hitPoint;


    WeaponTypeEnum.WeaponType currentWeaponType;
    PlayerController playerController;
    PlayerAnimatorHelper animatorHelper;
    public string[] meleeAttackStrings;//holds melee attack array of aniamtion strings

    public GameObject[] slots;
    GameObject currentSlot;
    WeaponDetails_SB currentWeapon;
    WeaponController currentWeaponController;


    float nextTimeToFire;
    void Start()
    {
        camera = GameManager.instance.camera;
        playerController = GetComponent<PlayerController>();
        animatorHelper = GetComponent<PlayerAnimatorHelper>();
        currentSlot = slots[0];
        currentWeaponType = WeaponTypeEnum.WeaponType.Melee;
    }
    void GetCrossHairPoint()
    {
        if (currentWeaponType == WeaponTypeEnum.WeaponType.Gun)//check if current weapon is a gun
        {
            if (currentWeaponController.projectileSpawnPoint != null)
            {
                Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;
                Debug.DrawRay(currentWeaponController.projectileSpawnPoint.position, camera.transform.forward * 50, Color.green);
                Debug.DrawRay(camera.transform.position, camera.transform.forward * 50, Color.green);

                if (Physics.Raycast(rayOrigin, camera.transform.forward, out hit, 50))
                {
                    hitPoint = hit.point;
                }
                else
                {
                    hitPoint = currentWeaponController.projectileSpawnPoint.position + camera.transform.forward * 100;
                }
            }
        }


    }

    void Update()
    {
        GetCrossHairPoint();
        Fire();
        SwitchWeapons();
    }


    void changeAnimation()//change animations according to weapon your holding
    {
        if (currentWeaponType == WeaponTypeEnum.WeaponType.Gun)
        {
            animatorHelper.GunMode();
        }
        if (currentWeaponType == WeaponTypeEnum.WeaponType.Melee)
        {
            animatorHelper.MeleeMode();
        }
        if (currentWeaponType == WeaponTypeEnum.WeaponType.OneHanded)
        {//TODOD 
        }
        if (currentWeaponType == WeaponTypeEnum.WeaponType.TwoHanded)
        {//TODOD 
        }
    }



    void Fire()
    {
        if (Input.GetButton("Fire1"))
        {
            if (currentWeaponType == WeaponTypeEnum.WeaponType.Gun && Time.time > nextTimeToFire)//check if gun
            {
                nextTimeToFire = Time.time + currentWeapon.fireRate;
                currentWeapon.projectileObj.GetComponent<BulletController>().damgeAmount = currentWeapon.damageAmount;
                GameObject bulletObj = Instantiate(currentWeapon.projectileObj);
                bulletObj.transform.position = currentWeaponController.projectileSpawnPoint.position;
                bulletObj.GetComponent<Rigidbody>().AddForce((hitPoint - bulletObj.transform.position).normalized * currentWeapon.bulletSpeed, ForceMode.Impulse);
            }

        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentWeaponType == WeaponTypeEnum.WeaponType.Melee && Time.time > nextTimeToFire)//check if melee
            {
                nextTimeToFire = Time.time + 0.2f;
                int attackNo = Random.Range(0, meleeAttackStrings.Length);
                animatorHelper.PlayAttack(meleeAttackStrings[attackNo]);
            }
        }
    }

    void SwitchWeapons()
    {
        //change weapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (currentSlot.transform.childCount != 0)//check wheather current slot has a weapon if so hide it
            {
                currentSlot.transform.GetChild(0).gameObject.SetActive(false);
            }
            currentSlot = slots[0];//set current slot as newley selected slot
            if (slots[0].transform.childCount != 0)
            {
                currentSlot.transform.GetChild(0).gameObject.SetActive(true);//activate the current slots gun
                currentWeapon = currentSlot.transform.GetChild(0).GetComponent<WeaponController>().weaponDetails;
                currentWeaponType = currentWeapon.weaponType;//set weapon type
                changeAnimation();//change animation 
            }
            else
            {
                currentWeaponType = WeaponTypeEnum.WeaponType.Melee;//if no weapon in slot go to melee mode
                changeAnimation();//change aniamtion

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (currentSlot.transform.childCount != 0)
            {
                currentSlot.transform.GetChild(0).gameObject.SetActive(false);
            }
            currentSlot = slots[1];
            if (slots[1].transform.childCount != 0)
            {
                currentSlot.transform.GetChild(0).gameObject.SetActive(true);

                currentWeapon = currentSlot.transform.GetChild(0).GetComponent<WeaponController>().weaponDetails;
                currentWeaponType = currentWeapon.weaponType;
                changeAnimation();

            }
            else
            {
                currentWeaponType = WeaponTypeEnum.WeaponType.Melee;
                changeAnimation();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (currentSlot.transform.childCount != 0)
            {
                currentSlot.transform.GetChild(0).gameObject.SetActive(false);
            }
            currentSlot = slots[2];
            if (slots[2].transform.childCount != 0)
            {
                currentSlot.transform.GetChild(0).gameObject.SetActive(true);
                currentWeapon = currentSlot.transform.GetChild(0).GetComponent<WeaponController>().weaponDetails;
                currentWeaponType = currentWeapon.weaponType;
                changeAnimation();

            }
            else
            {
                currentWeaponType = WeaponTypeEnum.WeaponType.Melee;
                changeAnimation();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Weapon")//check if weapon
        {
            if (currentSlot.transform.childCount == 0)
            {
                other.transform.parent = currentSlot.transform;
                other.transform.localPosition = Vector3.zero;
                other.transform.localRotation = Quaternion.identity;
                currentWeapon = other.transform.GetComponent<WeaponController>().weaponDetails;
                currentWeaponType = currentWeapon.weaponType;
                print(currentWeapon.weaponType);
                currentWeaponController = other.transform.GetComponent<WeaponController>();
                changeAnimation();

            }
        }
    }

}
