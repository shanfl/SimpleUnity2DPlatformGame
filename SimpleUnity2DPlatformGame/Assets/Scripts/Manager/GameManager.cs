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

    public Door doorExit;

    // 剩余敌人
    public List<Enemy> enemies = new List<Enemy>();

    public void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
        }

        player = FindObjectOfType<PlayerController>();

        doorExit = FindObjectOfType<Door>();
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

    public void NextLevel()
    {
        Debug.Log("next level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EnemyDead(Enemy enemy)
    {
        enemies.Remove(enemy);

        if(enemies.Count == 0)
        {           
            doorExit.OpenDoor();
            SaveData();
        }
    }

    public void IsEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public int LoadHealth()
    {
        if (!PlayerPrefs.HasKey("playerHealth"))
        {
            PlayerPrefs.SetInt("playerHealth", 3);
            return 3;
        }

        int tmp = PlayerPrefs.GetInt("playerHealth");
        return tmp;
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("playerHealth", player.health);
        PlayerPrefs.Save();
    }
}
