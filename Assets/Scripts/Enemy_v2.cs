using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_v2 : MonoBehaviour
{
    private Animator animator;
    private float followRange = 8f;
    private float stoppingDistance = 2f;
    private NavMeshAgent agent;
    private GameObject player;
    private Remy_New playerScript;
    private float distancePlayer;
    private Vector3 directionPlayer;
    private float waitBetweenAttacks = 0f;
    private float damage = 10;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<Remy_New>();
        }
    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 diff = player.transform.position - transform.position;
        distancePlayer = diff.magnitude;
        directionPlayer = diff.normalized;

        if (distancePlayer <= followRange)
        {
            if (distancePlayer <= stoppingDistance)
            {
                animator.SetBool("Walk", false);
                agent.isStopped = true;

                if (waitBetweenAttacks <= 0)
                {
                    Attack();
                    waitBetweenAttacks = 3f;
                }
                else
                {
                    animator.SetBool("Walk", false);
                    waitBetweenAttacks -= Time.deltaTime;
                }
            }
            else
            {
                animator.SetBool("Walk", true);
                agent.isStopped = false;
                GoToPlayer();
            }

        }
        else
        {
            animator.SetBool("Walk", false);
        }

    }

    private void GoToPlayer()
    { 
        agent.destination = player.transform.position;
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        playerScript.TakeDamage(damage);
    }

}
