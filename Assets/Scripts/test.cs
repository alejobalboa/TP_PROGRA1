using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    SoundController soundController;
    [SerializeField] private AudioClip musica;

    bool EmpezoAReproducirMusica = false;
    // Start is called before the first frame update
    void Start()
    {
        soundController = GetComponent<SoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!EmpezoAReproducirMusica)
        {
            EmpezoAReproducirMusica = true;
            soundController.PlaySoundLoop(musica);
        }
    }
}
