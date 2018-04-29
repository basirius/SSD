using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[HelpURL("https://www.youtube.com/channel/UCGrnFZJWOB769Pirppb8Xog")]
public class GameManager : UnitySingletonPersistent<GameManager>
{
    
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
    [HideInInspector]
    public float TiltAngle;
    [HideInInspector]
    public float TiltSmooth;
    #endregion

    #region Machine Gun Variables
    [HideInInspector]
    public float GunBaseDamage;
    [HideInInspector]
    public float FireRate;
    [HideInInspector]
    public GameObject[] Bullets;
    #endregion

    #region Music Variables
    [HideInInspector]
    public GameObject LevelMusicObject;
    #endregion

    // Machine Gun Variables

    public GameObject Settings;

    void Start()
    {
        Cursor.visible = true;
        // Tunnel Settings
        var levelSettings = Settings.GetComponent<Level01Settings>();
        this.SpawnInterval = levelSettings.SpawnInterval;
        this.TunnelSpawnInterval = levelSettings.TunnelSpawnInterval;
        this.StartSpawnDelay = levelSettings.SpawnStartDelay;
        this.LevelSpeed = levelSettings.LevelSpeed;
        this.TunnelSegment = levelSettings.TunnelSegment;
        this.SpawnGameObjects = levelSettings.SpawnGameObjects;
        this.TunnelSegmentLength = levelSettings.TunnelSegmentLength;

        // Ship Settings
        ShipSettings shipSettings = Settings.GetComponent<ShipSettings>();
        this.CurrentShield = shipSettings.CurrentShield;
        this.MaximumShield = shipSettings.MaximumShield;
        this.HorizontalMoveSpeed = shipSettings.HorizontalMoveSpeed;
        this.VerticalMoveSpeed = shipSettings.VerticalMoveSpeed;
        this.TiltAngle = shipSettings.TiltAngle;
        this.TiltSmooth = shipSettings.TiltSmooth;

        // Machine Gun Settings
        MachineGunSettings machineGunSettings = Settings.GetComponent<MachineGunSettings>();
        this.FireRate = machineGunSettings.FireRate;
        this.GunBaseDamage = machineGunSettings.Damage;
        this.Bullets = machineGunSettings.Bullets;

        foreach (var bullet in Bullets)
        {
            BulletBehaviour bulletBehaviour = bullet.GetComponent<BulletBehaviour>();
            bulletBehaviour.Damage = bulletBehaviour.BulletDamageModifier * GunBaseDamage;
        }

        // Music Settings
        this.LevelMusicObject = levelSettings.LevelMusicObject;

        // Load the Scene
        SceneManager.LoadScene("01 - Scenes/00 - StartMenu");
    }
}
