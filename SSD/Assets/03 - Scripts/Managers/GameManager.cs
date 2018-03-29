using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : UnitySingletonPersistent<GameManager> {

    // This is the main persistent singleton Game Manager
    // to use this in any script in the game gameManager = GameManager.Instance;
    [HideInInspector]
    public float LevelSpeed;
    [HideInInspector]
    public float SpawnInterval;
    [HideInInspector]
    public GameObject TunnelSegment;
    [HideInInspector]
    public float TunnelSegmentLength;

    public GameObject Settings;

    void Start()
    {
        Cursor.visible = true;
        Level01Settings level01Settings =  Settings.GetComponent<Level01Settings>();
        this.SpawnInterval = level01Settings.SpawnInterval;
        this.LevelSpeed = level01Settings.LevelSpeed;
        this.TunnelSegment = level01Settings.TunnelSegment;
        this.TunnelSegmentLength = level01Settings.TunnelSegmentLength;
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
