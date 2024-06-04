using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //BOTONES
    [SerializeField] private Button BtnIniciarPartida;
    [SerializeField] private Button BtnOpciones;
    [SerializeField] private Button BtnVolverAlMenuPrincipal;
    [SerializeField] private Button BtnSalir;
    
    [SerializeField] private Button ButtonPrefab;
    [SerializeField] private RectTransform ContenedorBotones;

    //PANTALLAS
    [SerializeField] private GameObject PantallaPrincipal;
    [SerializeField] private GameObject PantallaOpciones;

    [SerializeField] private Slider VolumenSFX;
    [SerializeField] private Slider VolumenBMG;

    private void Awake()
    {
        BtnIniciarPartida.onClick.AddListener(Jugar);
        BtnOpciones.onClick.AddListener(IrAOpciones);
        BtnVolverAlMenuPrincipal.onClick.AddListener(IrAMenuPrincipal);
        BtnSalir.onClick.AddListener(Salir);

        VolumenBMG.onValueChanged.AddListener(SetVolumenBMG);
        VolumenSFX.onValueChanged.AddListener(SetVolumenSFX);

        IrAMenuPrincipal();
    }
    private void Jugar()
    {
        Debug.Log("hola"); 
        GameManager.Instance.LoadLevel("Nivel1");
    }

    private void IrAMenuPrincipal()
    {
        PantallaPrincipal.SetActive(true);
        PantallaOpciones.SetActive(false);
    }

    private void IrAOpciones()
    {
        PantallaPrincipal.SetActive(false);
        PantallaOpciones.SetActive(true);
    }

    private void Salir()
    {
        Application.Quit();
    }

    private void SetVolumenSFX(float NuevoVolumen)
    {
        
    }

    private void SetVolumenBMG(float NuevoVolumen)
    {
        
    }



}
