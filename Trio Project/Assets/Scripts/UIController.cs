using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameObject pauseScreen;
    private Text debugText;

	void Start ()
    {
        pauseScreen.SetActive(false);
        debugText = GetComponentInChildren<Text>();
	}
	
	void Update ()
    {
        debugText.text = "Current Weapon: " + GameManager.Instance.currentWeapon.name + "\n";
        debugText.text += "Current Song: " + GameManager.Instance.currentSong.name + "\n";
        debugText.text += "Use 'W' and Mouse to move \nPress '1' to change colors \nPress '2' to change songs\nPress '3' to change weapons\n";
        debugText.text += "Press 'P' to pause the game.";

	}
}
