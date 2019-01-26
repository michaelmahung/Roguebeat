using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBuyUI : MonoBehaviour {

	public GameObject Player;
	public GameObject PurchaseUI;
	public bool WeaponScreenActive;
	private AudioLowPassFilter filter;
    private BaseWeapon selectedWeapon;
    public Text WeaponCostText;
	

	// Use this for initialization
	void Start () {
		WeaponScreenActive = false;
		PurchaseUI.SetActive(false);
		filter = AudioManager.Instance.Filter;
        selectedWeapon = PeaShooter.Instance;
	}
	
	// Update is called once per frame
	void Update () {
	//	print(Time.timeScale);
	if(Input.GetKeyDown(KeyCode.F) && WeaponScreenActive == true){
		PurchaseWeapons();
	}	
	}

	void OnTriggerEnter(){
if(Player){

            PurchaseWeapons();
}
	}
	public void PurchaseWeapons()
	{
		if(WeaponScreenActive == false)
		{
			WeaponScreenActive = true;
			PurchaseUI.SetActive(true);
			Time.timeScale = 0.0000001f;
			filter.enabled = true;
		}
		else{
			if(WeaponScreenActive == true)
			{
WeaponScreenActive = false;
			PurchaseUI.SetActive(false);
			Time.timeScale = 1;
			filter.enabled = false;
                GameManager.Instance.ResetPlayerPosition();

			}
		}
	}

    public void SelectWeapon(BaseWeapon weapon)
    {
        selectedWeapon = weapon;
        UpdateWeaponText();
    }

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