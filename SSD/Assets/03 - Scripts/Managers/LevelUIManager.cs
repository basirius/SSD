using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : UnitySingleton<LevelUIManager> {

    public Image CurrentShieldBar;
    [HideInInspector]
    public float CurrentShield;
    [HideInInspector]
    public float MaximumShield;
    private float shieldBarRatio;


	void Start () {
        UpdateUI();
    }
	
	void Update () {
        UpdateUI();
    }

    private void UpdateUI()
    {
        shieldBarRatio = CurrentShield / MaximumShield;
        CurrentShieldBar.rectTransform.localScale = new Vector3(shieldBarRatio, 1, 1);
    }
}
