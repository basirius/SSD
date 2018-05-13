using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelUIManager : UnitySingleton<LevelUIManager> {

    public Image CurrentShieldBar;
    public Text Dust_1;
    public Text Dust_2;
    public Text Dust_3;
    public Text Dust_4;
    public Text Dust_5;
    [HideInInspector]
    public float CurrentShield;
    [HideInInspector]
    public float MaximumShield;
    private float shieldBarRatio;
    private Canvas pauseMenu;
    private Canvas pauseMenuInstance;
    private bool gameIsPaused = false;

    private InventoryManager inventoryManager;
    private GameManager gameManager;

    void Start () {
        gameManager = GameManager.Instance;
        pauseMenu = gameManager.PauseMenu;
        inventoryManager = gameManager.Inventory.GetComponent<InventoryManager>();
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
        Dust_1.text = inventoryManager.DustMineral_1.ToString();
        Dust_2.text = inventoryManager.DustMineral_2.ToString();
        Dust_3.text = inventoryManager.DustMineral_3.ToString();
        Dust_4.text = inventoryManager.DustMineral_4.ToString();
        Dust_5.text = inventoryManager.DustMineral_5.ToString();
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
