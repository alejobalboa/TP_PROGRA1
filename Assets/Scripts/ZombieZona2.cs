using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieZona2 : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab; // que vamos a spawnear

    [SerializeField] private Remy_v2 player;
    private Vector3 spawnPosition;
    float spawnRadio = 0f; // Radio del area donde calcular  el spawn

    //float maxSpawn = 3; // cantidda Maxima a spawnear
    //[SerializeField] float SpawnInterval = 2f; //Intervalo de tiempo entre cada spwn


    
    private void OnTriggerEnter(Collider other)
    {
        Remy_v2 player = other.GetComponent<Remy_v2>();
        spawnPosition = transform.position + Random.insideUnitSphere * spawnRadio; // Genera una posicion aleatorea basada en el player
        //&& (!Physics.Raycast(spawnPosition, Vector3.up, 0.5f)))
        if (player != null) 

        {

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);


        }


        
    }
}
