using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : UnitySingletonPersistent<GameManager> {

    // This is the main persistent singleton Game Manager
    // to use this in any script in the game gameManager = GameManager.Instance;
    public float LevelSpeed;
    public float SpawnInterval;

    void Start()
    {
        Cursor.visible = true;
        
        SceneManager.LoadScene("01 - Scenes/00 - StartMenu");
        
    }

    void Update()
    {

    }

   //TODO
   /* TunnelBehavior expects to receive "Movement Speed - Spawn Interval - segment length
    * ...
    * For each level. These must come from each level setting file.
    */

}
