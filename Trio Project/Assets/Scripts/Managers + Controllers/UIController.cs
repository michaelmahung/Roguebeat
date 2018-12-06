using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour, IWeaponSwap, IChangeSong {

    public bool ShowText;
    public GameObject pauseScreen;
    private Text debugText;
    private string weaponName;
    private string songName;
    public bool gamePaused;
    private AudioLowPassFilter filter;

	void Start ()
    {
        filter = GameManager.Instance.filter;
        pauseScreen.SetActive(false);
        debugText = GetComponentInChildren<Text>();
        UpdateUIText();
	}

    public void WeaponSwapped()
    {
        weaponName = GameManager.Instance.Player.GetComponent<PlayerWeapon>().playerWeapon.name;
        UpdateUIText();
    }

    public void SongChanged()
    {
        UpdateUIText();
    }

	public void UpdateUIText()
    {
        if (ShowText)
        {
            debugText.text = "Left Click to Fire\n";
            debugText.text += "Current weapon: " + weaponName + "\n";
            debugText.text += "Tap 'Shift' to dash.\n";
            debugText.text += "Press '1' to change colors \n";
            debugText.text += "Press '2' to swap weapons\n";
            debugText.text += "Press 'Z' to pause the game.";
        } else
        {
            debugText.text = "";
        }

    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        try
        {
            if (!gamePaused)
            {
                gamePaused = true;
                pauseScreen.SetActive(true);
                Time.timeScale = 0;
                filter.enabled = true;
            }
            else
            {
                pauseScreen.SetActive(false);
                gamePaused = false;
                Time.timeScale = 1;
                filter.enabled = false;
            }
        }
        catch
        {
            gamePaused = false;
            Debug.LogWarning("Add a reference to the UIController in the GameManager to pause.");
        }

    }
}
