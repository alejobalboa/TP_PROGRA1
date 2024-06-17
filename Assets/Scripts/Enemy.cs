using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    SoundController soundController;
    [SerializeField] private AudioClip sonidoGrunido;

    //EJECUCIÓN ENTRE CADA SONIDO DEL ZOMBIE
    [SerializeField] private float tiempoEntreGrunidos = 5f;
    private float tiempoUltimoGrunido = 0f;

    [SerializeField] private float health;
    [SerializeField] private float damage;

    private Animator animator;
    private float followRange = 8f;
    private float stoppingDistance = 2f;
    private NavMeshAgent agent;
    private GameObject player;
    private RemyFP playerScript;
    private float distancePlayer;
    private float waitBetweenAttacks = 0f;
    private int collectibleMask;
    private bool targetChosen;
    private int attackingTarget;
    Collider[] collectiblesInRange;
    private bool muerto = false;
    private Vector3 colletiblePosition;

    private void Start()
    {
        playerScript = GameManager.Instance.GetPlayerInstance();
        soundController = GetComponent<SoundController>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        collectibleMask = 1 << LayerMask.NameToLayer("Collectible");
    }

    private void Update()
    {
        if(playerScript == null)
        {
            playerScript = GameManager.Instance.GetPlayerInstance();
        }

        Vector3 diff = playerScript.transform.position - transform.position;
        distancePlayer = diff.magnitude;

        //Reproduzco el grunido del zombie cada cieto tiempo.
        tiempoUltimoGrunido += Time.deltaTime;
        if (tiempoUltimoGrunido >= tiempoEntreGrunidos)
        {
            soundController.PlaySound(sonidoGrunido);
            tiempoUltimoGrunido = 0;
        }


        if (distancePlayer <= followRange && muerto == false)
        {
            if (!targetChosen)
            {
                targetChosen = true;
                collectiblesInRange = Physics.OverlapSphere(transform.position, followRange, collectibleMask);
                if(collectiblesInRange.Length > 0)
                {
                    attackingTarget = Random.Range(0, 2);
                }
                else
                {
                    attackingTarget = 0;
                }
            }
          
            switch (attackingTarget)
            {
                case 0: //Ataca al jugador
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
                            waitBetweenAttacks -= Time.deltaTime;
                        }
                    }
                    else
                    {
                        agent.isStopped = false;
                        GoToTarget(playerScript.transform.position);
                    }
                    break;
                case 1: //Ataca al primer coleccionable del array
                    if (collectiblesInRange[0] != null)
                    {
                        colletiblePosition = collectiblesInRange[0].gameObject.transform.position;
                    } else
                    {
                        colletiblePosition = transform.position;
                    }
                    GoToTarget(colletiblePosition);
                    if(transform.position == colletiblePosition)
                    {
                        targetChosen = false;
                    }
                    break;
            }

        }
        else
        {
            agent.isStopped = true;
            agent.ResetPath();
            if (muerto == false) { animator.SetBool("Walk", false); }
            targetChosen = false;
        }
    }

    private void GoToTarget(Vector3 targetPosition)
    {
        animator.SetBool("Walk", true);
        agent.destination = targetPosition;
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        playerScript.TakeDamage(damage);
    }

    public void ZombieTakeDamage(float takeDamage)
    {
        health -= takeDamage;
        
        if (health <= 0)
        {
            if (muerto == false)
            {
                animator.SetBool("IsDead",true);
                animator.SetTrigger("Die");
                Destroy(gameObject, 5f);
                agent.isStopped = true;
                agent.ResetPath();
                muerto = true;
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Bullet bala = collision.gameObject.GetComponent<Bullet>();
    //    if (bala != null)
    //    {
    //        // cargar el daño de l abala hacia el zombie
    //        ZombieTakeDamage(bala.Damage);
    //    }
    //}
}
