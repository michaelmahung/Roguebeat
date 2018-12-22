﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour, IWeaponSwap {

    public bool ShowText;
    public bool GamePaused;
    public GameObject PauseScreen;
    public Text UIText;
    public Text ScoreText;

    private string weaponName;
    private string songName;
    private AudioLowPassFilter filter;

	void Start ()
    {
        filter = AudioManager.Instance.Filter;
        PauseScreen.SetActive(false);
        UpdateUIText();

        PlayerHealth.PlayerKilled += PlayerKilledText;
        GameManager.Instance.PlayerRespawned += UpdateUIText;
        GameManager.Instance.ScoreAdded += UpdateScoreText;
	}

    public void WeaponSwapped()
    {
        weaponName = GameManager.Instance.Player.GetComponent<PlayerWeapon>().playerWeapon.name;
        UpdateUIText();
    }

    public void PlayerKilledText()
    {
        UIText.fontSize = 60;
        UIText.text = "Press 'R' to respawn";
    }

    public void UpdateScoreText()
    {
        ScoreText.text = "Current Score: " + GameManager.Instance.CurrentScore;
    }

	public void UpdateUIText()
    {
        if (ShowText)
        {
            UIText.fontSize = 30;
            UIText.text = "Left Click to Fire\n";
            UIText.text += "Current weapon: " + weaponName + "\n";
            UIText.text += "Tap 'Shift' to dash.\n";
            UIText.text += "Press '1' to change colors \n";
            UIText.text += "Press '2' to swap weapons\n";
            UIText.text += "Press 'Z' to pause the game.";
        } else
        {
            UIText.text = "";
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
            if (!GamePaused)
            {
                GamePaused = true;
                PauseScreen.SetActive(true);
                Time.timeScale = 0;
                filter.enabled = true;
            }
            else
            {
                PauseScreen.SetActive(false);
                GamePaused = false;
                Time.timeScale = 1;
                filter.enabled = false;
            }
        }
        catch
        {
            PauseScreen.SetActive(false);
            GamePaused = false;
            Time.timeScale = 1;
            filter.enabled = false;
        }

    }
}
