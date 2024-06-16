using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Character;

public class Weapon : MonoBehaviour
{
    [SerializeField] private SoundController soundController;

    [SerializeField] Bullet prefabMunicion;
    [SerializeField] private float bulletDelay = 0.5f;
    [SerializeField] private Transform shootingPoint;

    [SerializeField] private AudioClip Disparo; 

    private float currentBulletDelay;

    private void Shoot()
    {
        soundController.PlaySound(Disparo);
        currentBulletDelay = 0;
        Bullet bullet = Instantiate(prefabMunicion, shootingPoint.position, shootingPoint.rotation);

        bullet.SetDirection(shootingPoint.forward);
    }

    // Update is called once per frame
    void Update()
    {
        currentBulletDelay += Time.deltaTime;

        if (Input.GetButton("Fire1") && currentBulletDelay >= bulletDelay)
        {
            Shoot();
        }        
    }
}
