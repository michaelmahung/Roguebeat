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

	void Start ()
    {
        pauseScreen.SetActive(false);
        debugText = GetComponentInChildren<Text>();
        UpdateUIText();
	}

    public void WeaponSwapped()
    {
        weaponName = GameManager.Instance.player.GetComponent<PlayerWeapon>().playerWeapon.name;
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
            debugText.text = "Left Click to Fire\n" + "Current weapon: " + weaponName + "\n";
            debugText.text += "Press '1' to change colors \nPress '2' to swap weapons\n";
            debugText.text += "Tap 'Shift' to dash.\n";
            debugText.text += "Press 'Z' to pause the game.";
        } else
        {
            debugText.text = "";
        }

    }

	void Update ()
    {

	}
}
