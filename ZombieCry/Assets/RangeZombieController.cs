using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RangeZombieController : MonoBehaviour
{
    NavMeshAgent agent;
    RagDollHandler zombieRagDollHandler;
    Animator animator;
    Transform player;
    public float safeDistance;
    public float maxDistanceAwayFromPlayer;
    void Start()
    {
        zombieRagDollHandler = GetComponent<RagDollHandler>();
        animator = GetComponent<Animator>();
        player = GameManager.instance.player.GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
    }

    float distanceToPlayer()
    {
        return Vector3.Distance(this.transform.position, player.position);
    }
    void Update()
    {
        if (agent.enabled)
        {
            if (distanceToPlayer() <= safeDistance)//player close
            {
                agent.isStopped = false;
                Vector3 newDirection = -(player.position - this.transform.position).normalized * safeDistance;
                agent.SetDestination(newDirection);
            }
            else
            {
                agent.isStopped = true;
            }
        }
    }
}