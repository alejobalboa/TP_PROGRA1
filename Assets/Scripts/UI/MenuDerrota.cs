using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDerrota : MonoBehaviour
{
    SoundController soundController;
    //BOTONES
    [SerializeField] private Button BtnVolverAlMenuPrincipal;
    [SerializeField] private Button BtnSalir;

    //MUSICA MENU
    [SerializeField] private AudioClip Musica;
    [SerializeField] private AudioClip SonidoClick;

    bool EmpezoAReproducirMusica = false;

    private void Start()
    {
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
        BtnVolverAlMenuPrincipal.onClick.AddListener(IrAMenuPrincipal);
        BtnSalir.onClick.AddListener(Salir);
    }

    private void IrAMenuPrincipal() //Vuelvo a la escena del menú principal
    {
        soundController.PlaySound(SonidoClick);
        GameManager.Instance.LoadLevel("MainMenu");
    }

    private void Salir() //Que lastima pero Adios
    {
        soundController.PlaySound(SonidoClick);
        Application.Quit();
    }
}
