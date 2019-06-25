using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponDetails_SB weaponDetails;
    public Transform leftHandLocation;
    public Transform rightHandLocation;
    public Transform projectileSpawnPoint;
    void Start()
    {
        //left
        //right
        //barrell
        leftHandLocation = transform.GetChild(0);
        rightHandLocation = transform.GetChild(1);
        projectileSpawnPoint = transform.GetChild(2);
    }
}
