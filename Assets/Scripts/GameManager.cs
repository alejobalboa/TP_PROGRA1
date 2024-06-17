using System.Collections;
using System.Collections.Generic;
using System; //Solo lo importo para usar el try catch al cargar escenas.
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEditor.PackageManager;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => instance;
    private static GameManager instance;

    private RemyFP remyInstance;
    private int collectiblesInScene=5;
    private int collectiblesLost=0;
    private int collectiblesSave=0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void destroyedCollectible()
    {
        collectiblesLost++;
        checkCollectibles();
    }

    public void savedCollectible()
    {
        collectiblesSave++;
        checkCollectibles();
    }

    public void checkCollectibles()
    {
        if (collectiblesLost >= collectiblesInScene)
        {
            GameOver();
        } else {
            if ((collectiblesLost + collectiblesSave) == collectiblesInScene)
            {
                EndGame();
            }
        }  
    }

    public RemyFP GetPlayerInstance()
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

    public void LoadLevelAdditive(string sceneToLoad)
    {
        try
        {
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void PlayerCreated(RemyFP remy)
    {
        remyInstance = remy;
        remyInstance.OnDeathUnity.AddListener(GameOver);
    }

    public void GameOver()
    {
        remyInstance.OnDeathUnity.RemoveListener(GameOver);
        LoadLevel("PantallaDerrota");
    }

    public void EndGame()
    {
        LoadLevel("PantallaVictoria");
    }

    public int GetCollectiblesSave() { return collectiblesSave; }
    public int GetCollectiblesLost() { return collectiblesLost; }
    public int GetCollectiblesInScene() { return collectiblesInScene; }
}
