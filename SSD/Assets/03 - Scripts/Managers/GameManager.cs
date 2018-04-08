using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : UnitySingletonPersistent<GameManager> {

    // This is the main persistent singleton Game Manager
    // to use this in any script in the game : gameManager = GameManager.Instance;

    #region Tunnel Variables
    [HideInInspector]
    public float LevelSpeed;
    [HideInInspector]
    public float SpawnInterval;
    [HideInInspector]
    public float TunnelSpawnInterval;
    [HideInInspector]
    public GameObject TunnelSegment;
    [HideInInspector]
    public float TunnelSegmentLength;
    [HideInInspector]
    public float StartSpawnDelay;
    #endregion

    #region Spawn Game Objects
    [HideInInspector]
    public GameObject[] SpawnGameObjects;
    #endregion

    #region Ship Variables
    [HideInInspector]
    public float CurrentShield;
    [HideInInspector]
    public float MaximumShield;
    [HideInInspector]
    public float HorizontalMoveSpeed;
    [HideInInspector]
    public float VerticalMoveSpeed;
    #endregion

    // Machine Gun Variables

    public GameObject Settings;

    void Start()
    {
        Cursor.visible = true;
        // Tunnel Settings
        Level01Settings level01Settings =  Settings.GetComponent<Level01Settings>();
        this.SpawnInterval = level01Settings.SpawnInterval;
        this.TunnelSpawnInterval = level01Settings.TunnelSpawnInterval;
        this.StartSpawnDelay = level01Settings.SpawnStartDelay;
        this.LevelSpeed = level01Settings.LevelSpeed;
        this.TunnelSegment = level01Settings.TunnelSegment;
        this.SpawnGameObjects = level01Settings.SpawnGameObjects;
        this.TunnelSegmentLength = level01Settings.TunnelSegmentLength;
        

        // Ship Settings
        ShipSettings shipSettings = Settings.GetComponent<ShipSettings>();
        this.CurrentShield = shipSettings.CurrentShield;
        this.MaximumShield = shipSettings.MaximumShield;
        this.HorizontalMoveSpeed = shipSettings.HorizontalMoveSpeed;
        this.VerticalMoveSpeed = shipSettings.VerticalMoveSpeed;
        SceneManager.LoadScene("01 - Scenes/00 - StartMenu");
    }
}
