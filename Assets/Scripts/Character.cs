using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum SeleccionarDireccion
    {
        derecha,
        izquierda,
        adelante,
        atras
    };


    [SerializeField] private SeleccionarDireccion selDir = SeleccionarDireccion.derecha;
    [SerializeField] private float Speed; //Velocidad de desplazamiento
    [SerializeField] private float Direccion; //Direccion de movimiento 
    [SerializeField] private float NewScale; //Tamaño del modelo
    // Start is called before the first frame update


    void Start()
    {
        Vector3 NuevoTamanio;
        NuevoTamanio = transform.localScale;
        NuevoTamanio += new Vector3(NewScale, NewScale, NewScale);
        transform.localScale = NuevoTamanio;

        
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 PosicionActual = transform.position;
        Vector3 Derecha = transform.right;
        Vector3 Adelante = transform.forward;
        Vector3 Direccion;

        switch (selDir)
        {
            case SeleccionarDireccion.derecha:
                Derecha = transform.right * 1;
                Adelante = transform.forward * 0;
                break;
            case SeleccionarDireccion.izquierda:
                Derecha = transform.right * -1;
                Adelante = transform.forward * 0;
                break;
            case SeleccionarDireccion.atras:
                Derecha = transform.right * 0;
                Adelante = transform.forward * -1;
                break;
            case SeleccionarDireccion.adelante:
                Derecha = transform.right * 0;
                Adelante = transform.forward * 1;
                break;
            default:
                break;
        }
        Direccion = Derecha + Adelante;
        Direccion.y = 0;

        transform.position += Direccion.normalized * (Speed * Time.deltaTime);
    }
}
