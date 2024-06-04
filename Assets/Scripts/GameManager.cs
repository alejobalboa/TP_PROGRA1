using System.Collections;
using System.Collections.Generic;
using System; //Solo lo importo para usar el try catch al cargar escenas.
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
        try
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
}
