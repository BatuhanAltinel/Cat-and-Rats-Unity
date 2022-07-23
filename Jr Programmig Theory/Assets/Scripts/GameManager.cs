using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameScene;
    public GameObject gameOverScene;
    public GameObject ratPrefab;
    public GameObject trapPref;
    public GameObject liveUpPref;
    public GameObject finishScene;

    public Button pauseButton;
    public Button resumeButton;

    public Text ratText;
    public Text levelText;
    public Text livesText;

    public int levelIndex;
    private int numberOfRat;

    public bool isGameStarted;
    public ParticleSystem fireWork;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
       
    void Start()
    {
        InvokeRepeating("CreateLiveUp", 5f, 15f);
        levelIndex = 0;
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        gameScene.gameObject.SetActive(true);
        isGameStarted = false;
        Time.timeScale = 0.0f;
        pauseButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);
        gameScene.gameObject.SetActive(false);
        SoundManager.soundManager.StopTheMusic();
    }
    public void StartGame()
    {
        gameScene.gameObject.SetActive(false);
        gameOverScene.gameObject.SetActive(false);
        isGameStarted = true;
        Time.timeScale = 1.0f;
        pauseButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(false);
        SoundManager.soundManager.StartTheMusic();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        resumeButton.gameObject.SetActive(false);
        isGameStarted = true;
        pauseButton.gameObject.SetActive(true);
        SoundManager.soundManager.StartTheMusic();
    }
   
    public void SpawnRats()
    {
        for (int i = 0; i < levelIndex; i++)
        {
            Instantiate(ratPrefab, CreateSpawnPoint(0), Quaternion.identity);
        }
        
    }
    public Vector3 CreateSpawnPoint(float yPos)
    {
        Vector3 spawnPoint = new Vector3((Random.Range(-28, 28)), yPos, (Random.Range(-3, 14)));
        return spawnPoint;
    }

    void CreateTrap()
    {
        for (int i = 0; i < levelIndex; i++)
        {
            Instantiate(trapPref, CreateSpawnPoint(0), Quaternion.identity);
        }
        
    }

    public void NextLevel()
    {
        levelIndex++;
        levelText.text = "LEVEL " + levelIndex;
        CreateTrap();

        if (levelIndex == 11)
        {
            FinishScreen();
        }
        
    }
    public void CreateLiveUp()
    {
        Instantiate(liveUpPref, CreateSpawnPoint(0.8f), Quaternion.Euler(15,transform.position.y,transform.position.z));
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOverScene.gameObject.SetActive(true);
        gameScene.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        SoundManager.soundManager.StopTheMusic();
        isGameStarted = false;
        
    }
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void FinishScreen()
    {
        finishScene.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        gameScene.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        SoundManager.soundManager.StopTheMusic();
        isGameStarted = false;

    }
}
