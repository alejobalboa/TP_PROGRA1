using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class Bullet : MonoBehaviour
{
    public enum TipoMunicion { NM, FFS, AKFS };
    [SerializeField] private TipoMunicion selMun;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;
    private Enemy zombie;

    private Vector3 direction;

    private void Start()
    {
        switch (selMun)
        {
            case TipoMunicion.NM:
                speed = 20f;//Propiedades de la bala hardcodeadas.
                lifeTime = 5f;//Propiedades de la bala hardcodeadas.
                damage = 50f;//Propiedades de la bala hardcodeadas.
                break;
            case TipoMunicion.FFS:
                speed = 100f;//Propiedades de la bala hardcodeadas.
                lifeTime = 5f;//Propiedades de la bala hardcodeadas.
                damage = 70f;//Propiedades de la bala hardcodeadas.
                break;
            case TipoMunicion.AKFS:
                speed = 75f;//Propiedades de la bala hardcodeadas.
                lifeTime = 5f;//Propiedades de la bala hardcodeadas.
                damage = 80f;//Propiedades de la bala hardcodeadas.
                break;
            default:
                break;
        }
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0){Destroy(gameObject);}

        transform.position += direction * (speed * Time.deltaTime);
    }

    public void SetDirection(Vector3 moveDirection) 
    {
        direction = moveDirection; 
    }

    /*********************/
    /*****PROPIEDADES*****/
    /*********************/
    public float Speed
    {
        get{return speed;}
    }

    public float LifeTime
    {   
        get{return lifeTime;}
    }

    public float Damage
    {
        get { return damage; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Enemy zombie = collision.gameObject.GetComponent<Enemy>();
        if (zombie != null) 
        {
            // cargar el daño de l abala hacia el zombie
            zombie.ZombieTakeDamage(damage);
        }
    }

}
