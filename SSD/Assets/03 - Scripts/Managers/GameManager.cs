using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : UnitySingletonPersistent<GameManager> {

    // This is the main persistent singleton Game Manager
    // to use this in any script in the game : gameManager = GameManager.Instance;

    // Tunnel Variables
    [HideInInspector]
    public float LevelSpeed;
    [HideInInspector]
    public float SpawnInterval;
    [HideInInspector]
    public GameObject TunnelSegment;
    [HideInInspector]
    public float TunnelSegmentLength;

    // Ship Variables
    [HideInInspector]
    public float CurrentShield;
    [HideInInspector]
    public float MaximumShield;
    [HideInInspector]
    public float HorizontalMoveSpeed;
    [HideInInspector]
    public float VerticalMoveSpeed;

    public GameObject Settings;

    void Start()
    {
        Cursor.visible = true;
        // Tunnel Settings
        Level01Settings level01Settings =  Settings.GetComponent<Level01Settings>();
        this.SpawnInterval = level01Settings.SpawnInterval;
        this.LevelSpeed = level01Settings.LevelSpeed;
        this.TunnelSegment = level01Settings.TunnelSegment;
        this.TunnelSegmentLength = level01Settings.TunnelSegmentLength;

        // Ship Settings
        ShipSettings shipSettings = Settings.GetComponent<ShipSettings>();
        CurrentShield = shipSettings.CurrentShield;
        MaximumShield = shipSettings.MaximumShield;
        HorizontalMoveSpeed = shipSettings.HorizontalMoveSpeed;
        VerticalMoveSpeed = shipSettings.VerticalMoveSpeed;

        SceneManager.LoadScene("01 - Scenes/00 - StartMenu");
    }

    void Update()
    {

    }
}
