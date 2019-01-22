using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBuyUI : MonoBehaviour {
	public GameObject Player;
	public GameObject PurchaseUI;
	public bool WeaponScreenActive;
	 private AudioLowPassFilter filter;
	

	// Use this for initialization
	void Start () {
		WeaponScreenActive = false;
		PurchaseUI.SetActive(false);
		filter = AudioManager.Instance.Filter;

		
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

			}
		}
	}

	public void BuyShotgun(){
		if(GameManager.Instance.CurrentScore > 50){
print("Bought Shotgun!");
		}
		else
		{
			print("Costs 50 points, not enough to buy shotgun!");
		}
	}

	public void BuyLaser(){
		if(GameManager.Instance.CurrentScore > 100){
print("Bought Laser!");
		}
		else{
			print ("Costs 100 points, not enough to buy laser!");
		}
	}
	public void BuyFlamethrower(){
		if(GameManager.Instance.CurrentScore > 250){
		print("Bought Flamethrower!");
		}
		else
		{
		print("Costs 250 points, not enough to buy flamethrower!");
		}
	}
}