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
    
    [SerializeField] private Button ButtonPrefab;
    [SerializeField] private RectTransform ContenedorBotones;

    //PANTALLAS
    [SerializeField] private GameObject PantallaPrincipal;
    [SerializeField] private GameObject PantallaOpciones;

    [SerializeField] private Slider VolumenGeneral;

    //MUSICA MENU
    [SerializeField] private AudioClip Musica;

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

    private void SetVolumen(float NuevoVolumen)
    {
        PlayerPrefs.SetFloat("VolumenGeneral", NuevoVolumen);
    }
}
