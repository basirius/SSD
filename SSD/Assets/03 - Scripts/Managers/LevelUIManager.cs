using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUIManager : UnitySingleton<LevelUIManager> {

    public Image CurrentShieldBar;
    [HideInInspector]
    public float CurrentShield;
    [HideInInspector]
    public float MaximumShield;
    private float shieldBarRatio;
    private Canvas pauseMenu;
    private Canvas pauseMenuInstance;
    private bool gameIsPaused = false;

    private GameManager gameManager;

    void Start () {
        gameManager = GameManager.Instance;
        pauseMenu = gameManager.PauseMenu;
        pauseMenuInstance = Instantiate(pauseMenu, transform.position, Quaternion.identity);
        pauseMenuInstance.gameObject.SetActive(false);
        UpdateUI();
    }
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        shieldBarRatio = CurrentShield / MaximumShield;
        CurrentShieldBar.rectTransform.localScale = new Vector3(shieldBarRatio, 1, 1);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        pauseMenuInstance.gameObject.SetActive(false);
        gameIsPaused = false;
    }

    void Pause()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        pauseMenuInstance.gameObject.SetActive(true);
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        print("Loading the Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        gameIsPaused = false;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
