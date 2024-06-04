using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ZombieZona1 : MonoBehaviour
{
     [SerializeField] private Enemy enemyPrefab ; // que vamos a spawnear

     [SerializeField] private Remy_v2 player;

   
    private Vector3 spawnPosition;
    private float spawnRadio = 0f; // Radio del area donde calcular  el spawn
    
   
   
    private void OnTriggerEnter(Collider other)
    {
        Remy_v2 player = other.GetComponent<Remy_v2>();
        spawnPosition = transform.position + Random.insideUnitSphere * spawnRadio; // Genera una posicion aleatorea basada en el player

        if ((player != null) && (!Physics.Raycast(spawnPosition, Vector3.up, 0.5f)) )
           
        { 
                  
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            
                  
        }

        
        
    }

}
