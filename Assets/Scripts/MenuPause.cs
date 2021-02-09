//Права на данный курс принадлежат Дорофеевой Карине Олеговне, данный курс создавался для Udemy сайта
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class EnemyWaves
{
    // Time when the wave will appear on the stage
    public float timeToStart;
    // The Enemy Wave prefab to be spawned.
    public GameObject wave;
    // This wave after which the game will end.
    public bool is_Last_Wave;
}

public class MenuPause : MonoBehaviour
{
    
    public PlayerInventory PlayerInventory;
    // Static reference to the LevelController (can be used in other scripts).
    public static MenuPause instance;
    //Array of player ships
    public GameObject[] playerShip;
    // Reference to the EnemyWaves.
    public EnemyWaves[] enemyWaves;
    // Finish the game or not
    private bool is_Final = false;

    //відображення паузи
    public GameObject panel;
    //стан ігрової паузи
    private bool _isPause;
    //масив для зберігання кнопок (Exit, Return, Restart)
    public GameObject[] btnPause;

    private void Awake()
    {
        // Setting up the references.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        // Create all enemy waves...
        for (int i = 0; i < enemyWaves.Length; i++)
        {
            // Start CreateEnemyWave as a coroutine.
            //StartCoroutine(CreateEnemyWave(enemyWaves[i].timeToStart, enemyWaves[i].wave, enemyWaves[i].is_Last_Wave));
        }
    }
    private void Update()
    {

        // If is_Final = true, there are no objects with the Enemy tag left and the pause button is not pressed...
        if (is_Final == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !_isPause)
        {
            // якщо ми виграли
            
            GamePause();
            btnPause[1].SetActive(false);
        }
        if (Player.Instance == null && !_isPause)
        {
            // You lose game!
            
            GamePause();
            
            
        }
    }

    // Method pause
    public void GamePause()
    {
        // Call pause panel..
        if (!_isPause)
        {
            _isPause = true;
            // time in the game is frozen
            Time.timeScale = 0;
            //Show panel
            panel.SetActive(true);
            //If the player is not dead, the return button instead of the restart button...
            if (Player.Instance != null)
            {
                btnPause[0].SetActive(false);
                btnPause[1].SetActive(true);
            }
            //If the player is dead, the restart button instead of the return button...
            else
            {
                btnPause[0].SetActive(true);
                btnPause[1].SetActive(false);
            }
        }
        //Hide pause panel..
        else
        {
            _isPause = false;
            // time in the game is normal
            Time.timeScale = 1;
            //Hide panel
            panel.SetActive(false);
        }
    }
    //method restart game;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      //Права на данный курс принадлежат Дорофеевой Карине Олеговне, данный курс создавался для Udemy сайта
    public void BtnRestartGame()
    {
        PlayerInventory.coinsCount = 0;
         // time in the game is normal
         Time.timeScale = 1;
        //Loading current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //метод на кнопку Exit
    public void BtnExit()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }
}
//Права на данный курс принадлежат Дорофеевой Карине Олеговне, данный курс создавался для Udemy сайта