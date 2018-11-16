using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameObject pauseScreen;
    private Text debugText;

	void Start ()
    {
        //Set the pause screen to be false just in case
        pauseScreen.SetActive(false);
        debugText = GetComponentInChildren<Text>();
	}
	
	void Update ()
    {
        //Set debug text here: \n will create a new line.
        debugText.text =  "Left Click to Fire \n";
        debugText.text += "Current Song: " + GameManager.Instance.currentSong.name + "\n";
        debugText.text += "Use 'W' and Mouse to move \nPress '1' to change colors \nPress '2' to change songs\n";
        debugText.text += "Press 'P' to pause the game.";
	}
}
