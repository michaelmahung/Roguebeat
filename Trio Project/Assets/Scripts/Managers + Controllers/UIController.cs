using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour, IWeaponSwap, IChangeSong {

    public GameObject pauseScreen;
    private Text debugText;
    private string weaponName;
    private string songName;

	void Start ()
    {
        pauseScreen.SetActive(false);
        debugText = GetComponentInChildren<Text>();
        songName = GameManager.Instance.currentSong.name;
	}

    public void WeaponSwapped()
    {
        weaponName = GameManager.Instance.player.GetComponent<PlayerWeapon>().playerWeapon.name;
        UpdateUIText();
    }

    public void SongChanged()
    {
        songName = GameManager.Instance.currentSong.name;
        UpdateUIText();

    }

	public void UpdateUIText()
    {
        debugText.text = "Left Click to Fire\n" + "Current weapon: " + weaponName + "\n";
        debugText.text += "Current Song: " + songName + "\n";
        debugText.text += "Press '1' to change colors \nPress '2' to change songs\nPress '3' to swap weapons\n";
        debugText.text += "Tap 'Shift' to dash.\n";
        debugText.text += "Press 'P' to pause the game.";
    }

	void Update ()
    {

	}
}
