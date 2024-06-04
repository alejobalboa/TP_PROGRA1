using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RemyFP : MonoBehaviour
{

    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 12f;


    [SerializeField] private float health; //  Energia del jugador
    [SerializeField] private float maxHealth; // maxima capacidad de energia del jugador
    [SerializeField] private Weapon SelectedWeapon;

    public void Awake()
    {
        health = maxHealth;   
    }

    public void Update()
    {
        float moveLR = Input.GetAxis("Horizontal");
        float moveFB = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveLR + transform.forward * moveFB;

        controller.Move(move * speed *  Time.deltaTime);
    }

 
    public void TakeDamage(float damage) 
    {
        health -= damage;
        if (health <= 0) 
        {
            health = 0;
        }
    }
}
