using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 2);
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "enemy")
        {
            // if (!other.transform.GetComponent<WalkerZombieController>().dead)
            // {
            //     print("explo");
            // other.transform.GetComponent<Rigidbody>().isKinematic = false;
            // Collider[] colliders = Physics.OverlapSphere(transform.position, 50f);
            // foreach (Collider closeObjects in colliders)
            // {

            //     Rigidbody rb = closeObjects.GetComponent<Rigidbody>();
            //     if (rb != null)
            //     {
            //         if (closeObjects.transform.tag == "enemy")
            //         {

            //             rb.AddExplosionForce(700f, transform.position, 50);

            //         }
            //     }
            // }
            // Destroy(this.gameObject, 0);
            // }
        }

    }
}
