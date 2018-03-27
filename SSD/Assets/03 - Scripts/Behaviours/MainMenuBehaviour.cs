using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour {
	
	public void StartGame () {
        Cursor.visible = false;
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
    }
	
	public void Quit () {
        Application.Quit();
    }
}
