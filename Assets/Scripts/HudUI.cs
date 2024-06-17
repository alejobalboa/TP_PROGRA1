using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : MonoBehaviour
{
    [SerializeField] private Image lifeImage;
    [SerializeField] private GameObject neighbourTextObject; 
    private TextMeshProUGUI neighboursTextUGUI;
    private RemyFP remyInstance;
    private float currentLife;
    private float maxLife;
    private int neighboursRemaining;

    private void Start()
    {
        neighboursTextUGUI = neighbourTextObject.GetComponent<TextMeshProUGUI>();
        remyInstance = GameManager.Instance.GetPlayerInstance();
        maxLife = remyInstance.GetMaxHealth();
    }
    void Update()
    {
        neighboursRemaining = GameManager.Instance.GetCollectiblesInScene() - GameManager.Instance.GetCollectiblesLost() - GameManager.Instance.GetCollectiblesSave();
        currentLife = remyInstance.GetHealth();

        lifeImage.fillAmount = currentLife / maxLife;
        neighboursTextUGUI.text = "Vecinos restantes: " + neighboursRemaining.ToString();
    }
}
