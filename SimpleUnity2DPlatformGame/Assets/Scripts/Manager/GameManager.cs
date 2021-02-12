using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController player;

    public bool isGameOver;

    public void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
        }

        player = FindObjectOfType<PlayerController>();
    }


    void Update()
    {
        isGameOver = player.isDeath;
        UIManager.instance.GameOverUI(isGameOver);
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }    
}
