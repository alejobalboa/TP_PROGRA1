using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    public float distanciaInteraccion;
    public GameObject textoInteraccion;
    public string animacionPuertaAbierta, animacionPuertaCerrada;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distanciaInteraccion))
        {
            if (hit.collider.gameObject.tag == "Puerta")
            {
                GameObject doorParent = hit.collider.transform.parent.gameObject;
                Animator animacionPuerta = doorParent.GetComponent<Animator>();
                textoInteraccion.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {

                    if (animacionPuerta.GetCurrentAnimatorStateInfo(0).IsName(animacionPuertaAbierta))
                    {
                        animacionPuerta.ResetTrigger("abrir");
                        animacionPuerta.SetTrigger("cerrar");
                    }
                    if (animacionPuerta.GetCurrentAnimatorStateInfo(0).IsName(animacionPuertaCerrada))
                    {
                        animacionPuerta.ResetTrigger("cerrar");
                        animacionPuerta.SetTrigger("abrir");
                    }
                }
            }
            else
            {
                textoInteraccion.SetActive(false);
            }
        }
    }
}
