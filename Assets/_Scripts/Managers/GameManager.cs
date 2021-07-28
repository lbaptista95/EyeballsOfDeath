using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public delegate void GameEvents();
    public static event GameEvents OnPlayerDeath;
    public static GameManager instance = null;

    private string charSetPath;

    //[SerializeField] private float audioVolume;

    private SpriteSet spriteSet;
    public SpriteSet SpriteSet { get; }

    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject skinMenu;

    [SerializeField] private TMP_Text timeScoreText;
    [SerializeField] private TMP_Text projectilesScoreText;

    private int time;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        charSetPath = Path.Combine(Application.persistentDataPath, "char_set.json");
    }

    private void Start()
    {
        gameOver.SetActive(false);
        pauseMenu.SetActive(false);
        skinMenu.SetActive(false);
        GetSavedSpriteSet();
    }
    private void OnEnable()
    {
        SceneChangeManager.OnMainSceneLoaded += SceneChangeManager_OnMainSceneLoaded;
    }

    private void OnDisable()
    {
        SceneChangeManager.OnMainSceneLoaded -= SceneChangeManager_OnMainSceneLoaded;
    }

    private void SceneChangeManager_OnMainSceneLoaded()
    {
        Time.timeScale = 1;
        StopCoroutine(MatchTimer());
        StartCoroutine(MatchTimer());
    }
   
    #region SPRITES
    public SpriteSet GetSavedSpriteSet()
    {
        //If there's a saved sprite set, then load it. If there's no saved sprite set, create a new one.
        if (File.Exists(charSetPath))
        {
            spriteSet = JsonUtility.FromJson<SpriteSet>(File.ReadAllText(charSetPath));
        }
        else
        {
            spriteSet = new SpriteSet();
        }

        return spriteSet;
    }

    public void SaveCharacter(string json)
    {
        File.WriteAllText(charSetPath, json);
    }
    #endregion
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "GameScene")
            PauseGame();
    }

    #region GAME_MANAGEMENT

    private IEnumerator MatchTimer()
    {
        time = 0;
        while(true)
        {
            yield return new WaitForSeconds(1);
            time++;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;

        TimeSpan t = TimeSpan.FromSeconds(time);

        string timeFormatted = string.Format("{0:D2}:{1:D2}:{2:D2}",
                        t.Hours,
                        t.Minutes,
                        t.Seconds);

        timeScoreText.text = "Time passed: " + timeFormatted;
        projectilesScoreText.text = "Eyes: " + ProjectilesManager.instance.Projectiles;
        gameOver.SetActive(true);
       
    }

    public void Retry()
    {
        Time.timeScale = 1;

        

        gameOver.SetActive(false);
        SceneChangeManager.instance.GoToScene("GameScene");
    }

    public void OpenSkinMenu()
    {
        skinMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    #endregion
}
