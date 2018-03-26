﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : UnitySingletonPersistent<GameManager> {

    // This is the main persistent singleton Game Manager
    // to use this in any script in the game gameManager = GameManager.Instance;
    

    void Start()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("01 - Scenes/00 - StartMenu");
    }

    void Update()
    {

    }
   
}
