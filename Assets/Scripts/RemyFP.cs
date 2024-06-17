using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class RemyFP : MonoBehaviour
{

    [SerializeField] float speed = 5f;
    [SerializeField] private float health; //  Energia del jugador
    [SerializeField] private float maxHealth; // maxima capacidad de energia del jugador
    [SerializeField] private Weapon SelectedWeapon;

    public UnityEvent OnDeathUnity;

    public void Awake()
    {
        health = maxHealth;   
    }

    private void Start()
    {
        GameManager.Instance.PlayerCreated(this);
    }

    public void Update()
    {
        float moveLR = Input.GetAxis("Horizontal");
        float moveFB = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveLR + transform.forward * moveFB;

        transform.position += move * speed * Time.deltaTime;
    }

    public float GetHealth() {  return health; }
    public float GetMaxHealth() {  return maxHealth; }
 
    public void TakeDamage(float damage) 
    {
        health -= damage;
        if (health <= 0) 
        {
            OnDeathUnity?.Invoke();
            health = 0;
        }
    }
}
