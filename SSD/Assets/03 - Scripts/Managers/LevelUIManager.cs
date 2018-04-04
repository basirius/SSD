using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : UnitySingleton<LevelUIManager> {

    public Image CurrentShieldBar;
    private float currentShield = 150;
    private float maximumShield = 150;

	void Start () {
        GameManager gameManager = GameManager.Instance;

    }
	
	void Update () {
		
	}

    private void UpdateUI()
    {

    }
}
