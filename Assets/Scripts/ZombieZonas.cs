using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieZonas : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab; // que vamos a spawnear

    private RemyFP remyInstance;
    private Vector3 spawnPosition;
    float spawnRadio = 5f; // Radio del area donde calcular  el spawn

    private void OnTriggerExit(Collider other)
    {
        remyInstance = GameManager.Instance.GetPlayerInstance();
        spawnPosition = transform.position + Random.insideUnitSphere * spawnRadio; // Genera una posicion aleatorea basada en el player

        if (remyInstance != null)
        {
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }



    }
}
