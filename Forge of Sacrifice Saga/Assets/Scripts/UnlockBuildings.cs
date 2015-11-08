using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class UnlockBuildings : MonoBehaviour {
	
	[Header ("Upgrade Threshold")]
	public int Threshold;
	public int ThresholdGroth;
	
	public GameObject BuildPanel; 
	
	public List<Building> ActiveBuildings = new List<Building>();
	public List<Building> NotActiveBuildings = new List<Building>();

	
	public int farmUpgradeLvl = 1;
	public int woodcutterUpgradeLvl = 1;
	public int fisherUpgradeLvl = 1;
	public int churchUpgradeLvl = 1;
	
	private int humanAmount;
	
	private int buildingLvl = 0;
	private int playerLvl = 0;
	
	private List<Building> upgradeBuildings = new List<Building>();
	private List<Building.BuildingType> upgrades = new List<Building.BuildingType>();
	
	// Use this for initialization
	void Start () {
		humanAmount = 5;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (humanAmount > Threshold)
		{
			BuildPanel.SetActive(true);
			playerLvl++;
			
			if (playerLvl >= 5)
			{
				buildingLvl = 1;
			} else if (playerLvl >= 10)
			{
				buildingLvl = 2;
			}
			
			upgradeBuildings.Clear();
			upgrades.Clear();
			switch (buildingLvl)
			{
				case 0: 
					var builds = from building in NotActiveBuildings 
								where building.Type == Building.BuildingType.Farm || 
										building.Type == Building.BuildingType.Woodcutter 
								select building;
					upgradeBuildings.AddRange(builds);
					upgrades.Add(Building.BuildingType.Farm);
					upgrades.Add(Building.BuildingType.Woodcutter);
					break;
				case 1:
					var builds2 = from building in NotActiveBuildings 
								where building.Type == Building.BuildingType.Farm || 
										building.Type == Building.BuildingType.Woodcutter ||
										building.Type == Building.BuildingType.Fisher 
								select building;
					upgradeBuildings.AddRange(builds2);
					upgrades.Add(Building.BuildingType.Farm);
					upgrades.Add(Building.BuildingType.Woodcutter);
					upgrades.Add(Building.BuildingType.Fisher);
					break;
				case 2:
					var builds3 = from building in NotActiveBuildings 
								where building.Type == Building.BuildingType.Farm || 
										building.Type == Building.BuildingType.Woodcutter ||
										building.Type == Building.BuildingType.Fisher ||
										building.Type == Building.BuildingType.Church 
								select building;
					upgradeBuildings.AddRange(builds3);
					upgrades.Add(Building.BuildingType.Farm);
					upgrades.Add(Building.BuildingType.Woodcutter);
					upgrades.Add(Building.BuildingType.Fisher);
					upgrades.Add(Building.BuildingType.Church);
					break;
			}
			
			BuildPanel.GetComponent<ButtonController>().BuildingOptions2 = upgradeBuildings[Random.Range(0, upgradeBuildings.Count)];
			GameObject.Find("Option 2 Text").GetComponent<Text>().text = "Build new\n" + BuildPanel.GetComponent<ButtonController>().BuildingOptions2.Type.ToString() + "\n" + GetCostForType(BuildPanel.GetComponent<ButtonController>().BuildingOptions2.Type) + " Wood";
			
			BuildPanel.GetComponent<ButtonController>().BuildingOptions3 = upgradeBuildings[Random.Range(0, upgradeBuildings.Count)];
			GameObject.Find("Option 3 Text").GetComponent<Text>().text = "Build new\n" + BuildPanel.GetComponent<ButtonController>().BuildingOptions3.Type.ToString() + "\n" + GetCostForType(BuildPanel.GetComponent<ButtonController>().BuildingOptions3.Type) + " Wood";
			
			BuildPanel.GetComponent<ButtonController>().Upgrade1 = upgrades[Random.Range(0, upgrades.Count)];
			GameObject.Find("Option 1 Text").GetComponent<Text>().text = "Upgrade\n" + BuildPanel.GetComponent<ButtonController>().Upgrade1.ToString() + "s\nto +1\n" + GetCostForUpgrade(BuildPanel.GetComponent<ButtonController>().Upgrade1) + " Wood";
			 
			Threshold += ThresholdGroth;
			
		} else {
			BuildPanel.SetActive(false);
		}
	}
	
	public void GrantUpgrades(Building.BuildingType upgrade) 
	{
		List<Building> buildings = GetComponent<GameController>().Buildings;
		foreach (var building in buildings)
		{
			if (building.Type == upgrade) {
				building.Lvl += 1;
			}
			if (building.Type == Building.BuildingType.Farm)
			{
				farmUpgradeLvl += 1;
			} else if (building.Type == Building.BuildingType.Woodcutter)
			{
				woodcutterUpgradeLvl += 1;
			} else if (building.Type == Building.BuildingType.Fisher)
			{
				fisherUpgradeLvl += 1;
			} else if (building.Type == Building.BuildingType.Church)
			{
				churchUpgradeLvl += 1;
			}
		}
	}
	
	public int GetCostForType(Building.BuildingType buildType)
	{
		if (buildType == Building.BuildingType.Farm) 
        {
			return 50;    
        } else if (buildType == Building.BuildingType.Woodcutter)
        {
            return 80;
        } else if (buildType == Building.BuildingType.Fisher) 
        {
            return 150;
        } else
        {
            return 500;
        }
	}
	
	public int GetCostForUpgrade(Building.BuildingType buildType)
	{
		if (buildType == Building.BuildingType.Farm)
        {   
            return farmUpgradeLvl * 100;
        } else if (buildType == Building.BuildingType.Woodcutter)
        {
            return woodcutterUpgradeLvl * 100;
        } else if (buildType == Building.BuildingType.Fisher)
        {
            return fisherUpgradeLvl * 100;
        } else
        {
            return churchUpgradeLvl * 100;
        }
	}
}
