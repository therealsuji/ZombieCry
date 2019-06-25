using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponDetails_SB : ScriptableObject
{

    public GameObject weaponObj;
    public GameObject projectileObj;
    public new string name;
    public string description;
    public float damageAmount;
    public float fireRate;
    public int bulletSpeed;

    public WeaponTypeEnum.WeaponType weaponType;
    public Transform weaponPosition;



}
