using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        var gameManager = GameManager.Instance;
        if(other.CompareTag("Player"))
        {
            gameManager.savedCollectible();
        }
        if (other.CompareTag("Enemy"))
        {
            gameManager.destroyedCollectible();
        }
        Destroy(gameObject);
    }
}
