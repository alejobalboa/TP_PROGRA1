using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    SoundController soundController;
    //BOTONES
    [SerializeField] private Button BtnIniciarPartida;
    [SerializeField] private Button BtnOpciones;
    [SerializeField] private Button BtnVolverAlMenuPrincipal;
    [SerializeField] private Button BtnSalir;

    //PANTALLAS
    [SerializeField] private GameObject PantallaPrincipal;
    [SerializeField] private GameObject PantallaOpciones;

    [SerializeField] private Slider VolumenGeneral;

    //MUSICA MENU
    [SerializeField] private AudioClip Musica;
    [SerializeField] private AudioClip SonidoClick;

    bool EmpezoAReproducirMusica = false;

    private void Start()
    {
        VolumenGeneral.value = PlayerPrefs.GetFloat("VolumenGeneral", 0.3f);
        soundController = GetComponent<SoundController>();
    }

    private void Update()
    {
        if (!EmpezoAReproducirMusica)
        {
            EmpezoAReproducirMusica = true;
            soundController.PlaySoundLoop(Musica);
        }
    }

    private void Awake()
    {
        BtnIniciarPartida.onClick.AddListener(Jugar);
        BtnOpciones.onClick.AddListener(IrAOpciones);
        BtnVolverAlMenuPrincipal.onClick.AddListener(IrAMenuPrincipal);
        BtnSalir.onClick.AddListener(Salir);

        VolumenGeneral.onValueChanged.AddListener(SetVolumen);

        //No invoco el metodo para que no reproduzca el efecto de sonido del click
        PantallaPrincipal.SetActive(true);
        PantallaOpciones.SetActive(false);
    }

    private void Jugar()
    {
        soundController.PlaySound(SonidoClick);
        GameManager.Instance.LoadLevel("Level1SuperMarket");
    }

    private void IrAOpciones()
    {
        soundController.PlaySound(SonidoClick);
        PantallaPrincipal.SetActive(false);
        PantallaOpciones.SetActive(true);
    }

    private void IrAMenuPrincipal()
    {
        soundController.PlaySound(SonidoClick);
        PantallaPrincipal.SetActive(true);
        PantallaOpciones.SetActive(false);
    }

    private void Salir()
    {
        soundController.PlaySound(SonidoClick);
        Application.Quit();
    }

    private void SetVolumen(float NuevoVolumen)
    {
        PlayerPrefs.SetFloat("VolumenGeneral", NuevoVolumen);
    }
}