using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBuyUI : MonoBehaviour {

	[SerializeField] private GameObject PurchaseUI;
    [SerializeField] private bool WeaponScreenActive;
    [SerializeField] private Text WeaponCostText;
    [SerializeField] private Text WeaponCostText1;

    private GameObject Player;
    private AudioLowPassFilter filter;
    private BaseWeapon selectedWeapon;


    // Use this for initialization
    void Start () {
        FindComponents();
        PlayerHealth.PlayerKilled += ToggleWeaponBuyUI;
        Player = GameManager.Instance.PlayerObject;
		WeaponScreenActive = false;
		PurchaseUI.SetActive(false);
		filter = SFXManager.Instance.Filter;
        selectedWeapon = PeaShooter.Instance;
	}
	
	public void ToggleWeaponBuyUI()
	{
		if(WeaponScreenActive == false)
		{
            OpenWeaponUI();
		}
		else if (WeaponScreenActive == true)
        {
            CloseWeaponUI();
        }
	}

    void CloseWeaponUI()
    {
        WeaponScreenActive = false;
        PurchaseUI.SetActive(false);
        Time.timeScale = 1;
        filter.enabled = false;

        if(GameManager.Instance.IsPlayerDead)
        {
            GameManager.Instance.RespawnPlayer();
            return;
        }

        GameManager.Instance.ResetPlayerPosition();
        return;
    }

    void OpenWeaponUI()
    {
        WeaponScreenActive = true;
        PurchaseUI.SetActive(true);
        Time.timeScale = 0.0000001f;
        filter.enabled = true;
    }

    //This is the function that should be linked to the individual buttons in the UI
    public void SelectWeapon(BaseWeapon weapon)
    {
        selectedWeapon = weapon;
        UpdateWeaponText();
    }

    //This is the function that should be called when pressing the Purchase Weapon button
    public void BuyWeapon()
    {
        if (!selectedWeapon.WeaponActive)
        {
            if (GameManager.Instance.CurrentScore >= selectedWeapon.WeaponCost)
            {
                //Debug.Log("Purchased " + selectedWeapon.name);
                selectedWeapon.SetWeaponActive(true);
                return;
            }
            else
            {
                //Debug.LogFormat("{0} costs {1} points! You need {2} more.", selectedWeapon.name, selectedWeapon.WeaponCost, (selectedWeapon.WeaponCost - GameManager.Instance.CurrentScore));
                return;
            }
        }

        //Debug.Log("You already own " + selectedWeapon.name);
    }

    void UpdateWeaponText()
    {
        WeaponCostText.text = string.Format("{0} costs {1} points", selectedWeapon.name, selectedWeapon.WeaponCost);
        WeaponCostText1.text = string.Format("{0} costs {1} points", selectedWeapon.name, selectedWeapon.WeaponCost);

    }

    void FindComponents()
    {
        if (PurchaseUI == null)
        {
            PurchaseUI = GameObject.Find("Upgrades UI");
        }

        if (WeaponCostText == null)
        {
            WeaponCostText = GameObject.Find("Weapon Cost Text").GetComponent<Text>();
            WeaponCostText1 = GameObject.Find("Weapon Cost Text1").GetComponent<Text>();
        }
    }
}