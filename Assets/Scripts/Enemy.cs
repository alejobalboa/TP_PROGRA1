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

    [SerializeField] private float health = 250f;
    [SerializeField] private float Currenthealth;
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

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<RemyFP>();
        }
    }

    private void Start()
    {
        soundController = GetComponent<SoundController>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        collectibleMask = 1 << LayerMask.NameToLayer("Collectible");
    }

    private void Update()
    {
        Vector3 diff = player.transform.position - transform.position;
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
                        GoToTarget(player.transform.position);
                    }
                    break;
                case 1: //Ataca al primer coleccionable del array
                    
                    GoToTarget(collectiblesInRange[0].gameObject.transform.position);
                    if(transform.position == collectiblesInRange[0].gameObject.transform.position)
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
        Currenthealth -= takeDamage;
        
        if (Currenthealth <= 0)
        {
            if (muerto == false)
            {
                animator.SetTrigger("Die");
                Destroy(gameObject, 5f);
                muerto = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bullet bala = collision.gameObject.GetComponent<Bullet>();
        if (bala != null)
        {
            // cargar el daño de l abala hacia el zombie
            ZombieTakeDamage(bala.Damage);
        }
    }
}
