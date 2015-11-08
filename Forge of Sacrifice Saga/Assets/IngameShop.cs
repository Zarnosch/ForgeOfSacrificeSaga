using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IngameShop : MonoBehaviour {
	
	public GameObject IngameShopPanel;
	public Text SchruKoeCount;
	private int schruKoe;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OpenIngameShop() {
		IngameShopPanel.SetActive(true);
		SchruKoeCount.text = "You own: " + schruKoe + " SchruKö";
	}
	
	public void CloseIngameShop() {
		IngameShopPanel.SetActive(false);
	}
	
	public void GetSchrKoe() {
		schruKoe += 499;
		SchruKoeCount.text = "You own: " + schruKoe + " SchruKö";
	}
	
	public void GetFarmManager() {
		if (schruKoe > 1000)
		{
			schruKoe -= 1000;
			Debug.Log("Jay FarmManager");
		}
		SchruKoeCount.text = "You own: " + schruKoe + " SchruKö";
	}
	
	public void GetSpeed() {
		if (schruKoe > 500)
		{
			schruKoe -= 500;
			Debug.Log("SPEEEEED");
		}
		SchruKoeCount.text = "You own: " + schruKoe + " SchruKö";
	}
	
	public void GetCannibalism() {
		if (schruKoe > 200)
		{
			schruKoe -= 200;
			Debug.Log("Nom MEAT");
		}
		SchruKoeCount.text = "You own: " + schruKoe + " SchruKö";
	}
}
