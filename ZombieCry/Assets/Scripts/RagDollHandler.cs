using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagDollHandler : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject weapon;
    void Start()
    {
        setRigidBodyState(true);
        setColliderState(false);
        agent = GetComponent<NavMeshAgent>();

    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {

    }

    public void Die()
    {
        if (weapon != null)
        {
            weapon.GetComponent<Rigidbody>().isKinematic = false;
            weapon.GetComponent<BoxCollider>().isTrigger = false;

            weapon.GetComponent<Rigidbody>().useGravity = true;

        }
        if (agent != null)
        {
            agent.enabled = false;

        }
        GetComponent<Animator>().enabled = false;
        setRigidBodyState(false);
        setColliderState(true);
    }

    void setRigidBodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = state;
        }
        //  GetComponent<Rigidbody>().isKinematic = !state;
    }
    void setColliderState(bool state)
    {
        Collider[] rigidbodies = GetComponentsInChildren<Collider>();
        foreach (Collider collider in rigidbodies)
        {
            collider.enabled = state;
            if (collider.transform.tag == "attack1")
            {
                collider.enabled = !state;
                collider.isTrigger = !state;
            }
        }
        GetComponent<Collider>().enabled = !state;

    }

}
