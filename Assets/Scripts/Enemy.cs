using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float pursuitThreshold;
    [SerializeField] private float fleeThreshold;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float zombieDamage;
    [SerializeField] private float zombieEnergy;
    [SerializeField] private Animator animator;
    [SerializeField] private float damage; // daño que se le provoca al player
    [SerializeField] private AudioSource zsfx;
    [SerializeField] private AudioClip zWalk;
   
    private GameObject playerObject;
    private Vector3 playerPosition;
    
    // Start is called before the first frame update
    private void Awake()
    {

        
        playerObject = GameObject.FindWithTag("Player");
        
               
    }
    void Start()
    {
        
        

    }

   
    void Update()
    {
  
       PursuitPlayer();
    }
    
    
    private void PursuitPlayer() 
    {
        
        Vector3 diff = playerObject.transform.position - transform.position;
        float distance = diff.magnitude;
        Vector3 directionToPlayer = diff.normalized;
                   

        if (distance > pursuitThreshold) 
        {
            animator.SetFloat("Zombie_Speed", 0);
            return;
        }
        
        transform.position += directionToPlayer * (Time.deltaTime * speed);      
        
        animator.SetFloat("Zombie_Speed", distance);
    
        LookAtPlayerOwnRotation();
    }

    
    private void LookAtPlayerOwnRotation()
    {
        Quaternion newRotation = Quaternion.LookRotation(playerObject.transform.position - transform.position);// esta es mi nueva rotacion para mirar al player
        Quaternion currentRotation = transform.rotation;
        Quaternion finalRotation = Quaternion.Lerp(currentRotation,newRotation,rotationSpeed * Time.deltaTime);
        transform.rotation = finalRotation;
    }
    private void EscapeFromPlayer()
    {
        Vector3 diff = transform.position - playerObject.transform.position;
        float distance = diff.magnitude;
        diff.y = 0;
        Vector3 directionOppositeToPlayer = diff.normalized;

        if (distance > fleeThreshold)
        {
            return;
        }
        //  probar y despues hacer que la velocidad de escape sea otra mas lenta
        transform.position += directionOppositeToPlayer * (Time.deltaTime * speed);

    }

    private void OnCollisionStay(Collision other)
    {
        Remy_v2 player = other.gameObject.GetComponent<Remy_v2>();

        if (player != null) 
       
        {
   
            player.TakeDamage(damage * Time.fixedDeltaTime);
           
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
       Remy_v2 player = collision.gameObject.GetComponent<Remy_v2>();
               
        if (player != null)
        {

            animator.SetFloat("Zombie_Speed", 0);
            zsfx.PlayOneShot(zWalk);

        }
      
    }
    public void ZombieTakeDamage(float zombieDamage) 
    {
        zombieEnergy -= zombieDamage;
        Debug.Log("Zombie Health:" + zombieEnergy);
        if (zombieEnergy <= 0)
        {
            //animator.SetFloat("Zombie_Speed", 0);
            animator.SetTrigger("Dye");
            Destroy(gameObject);
        }
    }

}
