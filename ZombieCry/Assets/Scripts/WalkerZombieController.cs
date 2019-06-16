using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkerZombieController : MonoBehaviour
{
    Animator animator;
    Transform player;
    NavMeshAgent agent;
    public bool attack;
    public bool run;
    public bool chase;
    public bool dead;
    public int health = 100;
    [Range(0f, 1000f)]
    public float angularSpeed = 1000f;
    RagDollHandler zombieRagDollHandler;
    public float attackDist;


    float time;
    void Start()
    {
        zombieRagDollHandler = GetComponent<RagDollHandler>();
        animator = GetComponent<Animator>();
        player = GameManager.instance.player.GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        angularSpeed = 1000f;

    }



    void FixedUpdate()
    {
        if (agent.enabled)
        {
            checkPlayerRaycast();
            agent.angularSpeed = angularSpeed;
            //apply rootmotion when attacking
            animator.applyRootMotion = animator.GetBool("isAttacking");
            if (chase)
            {
                if (agent.enabled && player != null)
                {
                    agent.isStopped = false;
                    agent.SetDestination(player.position);
                    agent.stoppingDistance = 1.9f;
                }
            }
            else
            {
                agent.isStopped = true;
            }



            if (dead || health == 0)
            {
                zombieRagDollHandler.Die();
            }
            if (GameManager.instance.playerIsDead)
            {
                agent.isStopped = true;
                stopAttackAnimation();
                animator.SetBool("idle", true);
            }
        }

    }



    void playAttackAnimation()
    {
        animator.SetBool("attack", true);
    }
    void stopAttackAnimation()
    {
        animator.SetBool("attack", false);
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "bullet")
        {

            health -= 10;
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
        // Destroy(this.gameObject, 0);
    }

    void checkPlayerRaycast()
    {

        if (Vector3.Distance(this.transform.position, player.transform.position) < 1.5)
        {

            playAttackAnimation();

        }
        else
        {
            stopAttackAnimation();
        }
    }

}
