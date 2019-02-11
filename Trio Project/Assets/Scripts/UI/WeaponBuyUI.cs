using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBuyUI : MonoBehaviour {

	[SerializeField] private GameObject PurchaseUI;
    [SerializeField] private bool WeaponScreenActive;
    [SerializeField] private Text WeaponCostText;

    private GameObject Player;
    private AudioLowPassFilter filter;
    private BaseWeapon selectedWeapon;


    // Use this for initialization
    void Start () {
        PlayerHealth.PlayerKilled += ToggleWeaponBuyUI;
        Player = GameManager.Instance.PlayerObject;
		WeaponScreenActive = false;
		PurchaseUI.SetActive(false);
		filter = SFXManager.Instance.Filter;
        selectedWeapon = PeaShooter.Instance;
	}
	
	// Update is called once per frame
	void Update () {
	//	print(Time.timeScale);
	if(Input.GetKeyDown(KeyCode.F) && WeaponScreenActive == true){
		ToggleWeaponBuyUI();
	}	
	}

	void OnTriggerEnter(Collider other){
if(other.tag == "Player"){

            ToggleWeaponBuyUI();
}
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
                Debug.Log("Purchased " + selectedWeapon.name);
                selectedWeapon.SetWeaponActive(true);
                return;
            }
            else
            {
                Debug.LogFormat("{0} costs {1} points! You need {2} more.", selectedWeapon.name, selectedWeapon.WeaponCost, (selectedWeapon.WeaponCost - GameManager.Instance.CurrentScore));
                return;
            }
        }

        Debug.Log("You already own " + selectedWeapon.name);
    }

    void UpdateWeaponText()
    {
        WeaponCostText.text = string.Format("{0} costs {1} points", selectedWeapon.name, selectedWeapon.WeaponCost);
    }

	/*public void BuyShotgun(){
		if(GameManager.Instance.CurrentScore > 50){
            Shotgun.Instance.SetWeaponActive(true);
            PurchaseWeapons();
            print("Bought Shotgun!");
		}
		else
		{
			print("Costs 50 points, not enough to buy shotgun!");
		}
	}

	public void BuyLaser(){
		if(GameManager.Instance.CurrentScore > 100){
            LaserRifle.Instance.SetWeaponActive(true);
            PurchaseWeapons();
            print("Bought Laser!");
		}
		else{
			print ("Costs 100 points, not enough to buy laser!");
		}
	}
	public void BuyFlamethrower(){
		if(GameManager.Instance.CurrentScore > 250){
            Flamethrower.Instance.SetWeaponActive(true);
            PurchaseWeapons();
		print("Bought Flamethrower!");
		}
		else
		{
		print("Costs 250 points, not enough to buy flamethrower!");
		}
	}*/
}