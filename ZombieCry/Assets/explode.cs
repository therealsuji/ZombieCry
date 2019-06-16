using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "bullet")
        {
            Explosition(other.transform.position);
        }

    }
    void Explosition(Vector3 pos)
    {
        Collider[] colliders = Physics.OverlapSphere(pos, 50f);
        foreach (Collider closeObjects in colliders)
        {

            Rigidbody rb = closeObjects.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(700f, pos, 50);
            }
        }
        //Destroy(this.gameObject, 0);
    }
}
