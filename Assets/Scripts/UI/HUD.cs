using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image rescueBar;
    [SerializeField] private Image rescueBarLost;
    private RemyFP remyInstance;

    //private Items itemCount;

    RemyFP player;

    private float maxHealth;
    private float currentHealth;


    // Start is called before the first frame update
    private void Awake()
    {



    }
    void Start()
    {
        //canvas = GameObject.Find("Canvas").GetComponent<Canvas>(); //Aca voy a buscar el canvas
        //healthBar = canvas.GetComponentInChildren<Image>(); // Obetener los componentes image

        remyInstance = GameManager.Instance.GetPlayerInstance();
        maxHealth = remyInstance.GetMaxHealth();


    }

    void Update()
    {
        HealthBar();
        RescueBar();
        RescueBarLost();
    }

    private void HealthBar()
    {
        currentHealth = remyInstance.GetHealth();
        healthBar.fillAmount = currentHealth / maxHealth;
    }
    private void RescueBar()
    {

        rescueBar.fillAmount = (GameManager.Instance.GetCollectiblesSave() * 1f) / GameManager.Instance.GetCollectiblesInScene();
    }
    private void RescueBarLost()
    {

        rescueBarLost.fillAmount = (GameManager.Instance.GetCollectiblesLost() * 1f) / GameManager.Instance.GetCollectiblesInScene();
    }

    //private void EnemyCounter() 
    //{
    //    enemyCount = 0;
    //    foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
    //    {
    //      enemyCount++;
    //    }
    //}

}
