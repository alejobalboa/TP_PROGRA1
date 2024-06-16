using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieZona2 : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab; // que vamos a spawnear

    [SerializeField] private Remy_New player;
    private Vector3 spawnPosition;
    float spawnRadio = 0f; // Radio del area donde calcular  el spawn
       
    private void OnTriggerExit(Collider other)
    {
        
        Remy_New player = other.GetComponent<Remy_New>();
        spawnPosition = transform.position + Random.insideUnitSphere * spawnRadio; // Genera una posicion aleatorea basada en el player
        //&& (!Physics.Raycast(spawnPosition, Vector3.up, 0.5f)))
        if (player != null) 

        {

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);


        }


        
    }
}
