using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => instance;
    private static GameManager instance;

    private Remy remyInstance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else //Instance != null
        {
            Destroy(gameObject);
        }
    }

    public Remy GetPlayerInstance()
    {
        return remyInstance;
    }

    public void LoadLevel(string sceneToLoad)
    {
        //Agregar despues cuando tengamos los niveles.
        //SceneManager.LoadScene(sceneToLoad);
    }



}
