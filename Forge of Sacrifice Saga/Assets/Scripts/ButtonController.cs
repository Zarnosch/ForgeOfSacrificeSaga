using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {
    public GameObject popup;
    [HideInInspector]
    public GameObject SelectedBuilding;
    public GameController GameController;
    
    [HideInInspector]
    public Building BuildingOptions2;
    [HideInInspector]
    public Building BuildingOptions3;
    [HideInInspector]
    public Building.BuildingType Upgrade1;
    
	// Use this for initialization
	void Start () {
        GameController = GameObject.Find("GameController").GetComponent<GameController>();
        SelectedBuilding = GameObject.Find("MainBuilding");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void closeButton()
    {
        popup.SetActive(false);
    }

    public void AssignWorker()
    {
        if(SelectedBuilding.GetComponent<Building>().CurrentWorker < SelectedBuilding.GetComponent<Building>().MaxWorker && GameController.GetFreeHumans().Count > 0)
        {
            SelectedBuilding.GetComponent<Building>().CurrentWorker += 1;
            GameController.FreeHumansL[0].IsWorking = true;
            GameController.FreeHumansL[0].SetNewTarget(SelectedBuilding.GetComponent<Building>());
        }
        GameController.UpdateHumans();
        SelectedBuilding.GetComponent<clickforhouseinfos>().updateInfo();
    }

    public void RemoveWorker()
    {
        if (SelectedBuilding.GetComponent<Building>().CurrentWorker > 0)
        {
            foreach(HumanController human in GameController.WorkingHumansL)
            {
                if (human.targetBuilding.Equals(SelectedBuilding.GetComponent<Building>()) && human.IsWorking)
                {
                    human.IsWorking = false;
                    human.SetNewTarget(GameObject.Find("MainBuilding").GetComponent<Building>());
                    SelectedBuilding.GetComponent<Building>().CurrentWorker -= 1;
                    break;
                }
            }
        }
        GameController.UpdateHumans();
        SelectedBuilding.GetComponent<clickforhouseinfos>().updateInfo();
    }
    
    public void SelectBuildOption1() {
        UnlockBuildings ub = GameController.GetComponent<UnlockBuildings>();
        GameController gc = GameController.GetComponent<GameController>();
        if (Upgrade1 == Building.BuildingType.Farm)
        {   
            if (gc.Wood > ub.farmUpgradeLvl * 100)
            {
                gc.Wood -= ub.farmUpgradeLvl * 100;
                GameController.GetComponent<UnlockBuildings>().GrantUpgrades(Upgrade1);    
            }
        } else if (Upgrade1 == Building.BuildingType.Woodcutter)
        {
            if (gc.Wood > ub.woodcutterUpgradeLvl * 100)
            {
                gc.Wood -= ub.woodcutterUpgradeLvl * 100;
                GameController.GetComponent<UnlockBuildings>().GrantUpgrades(Upgrade1);    
            }
        } else if (Upgrade1 == Building.BuildingType.Fisher)
        {
            if (gc.Wood > ub.fisherUpgradeLvl * 100)
            {
                gc.Wood -= ub.fisherUpgradeLvl * 100;
                GameController.GetComponent<UnlockBuildings>().GrantUpgrades(Upgrade1);    
            }
        } else if (Upgrade1 == Building.BuildingType.Church)
        {
            if (gc.Wood > ub.churchUpgradeLvl * 100)
            {
                gc.Wood -= ub.churchUpgradeLvl * 100;
                GameController.GetComponent<UnlockBuildings>().GrantUpgrades(Upgrade1);    
            }
        }
    }
    
    public void SelectBuildOption2() {
        GameController gc = GameController.GetComponent<GameController>();
        if (BuildingOptions2.Type == Building.BuildingType.Farm) 
        {
            if (gc.Wood > 50)
            {
                gc.Wood -= 50;
                BuildingOptions2.IsActive = true;
            }    
        } else if (BuildingOptions2.Type == Building.BuildingType.Woodcutter)
        {
            if (gc.Wood > 80)
            {
                gc.Wood -= 80;
                BuildingOptions2.IsActive = true;
            }    
        } else if (BuildingOptions2.Type == Building.BuildingType.Fisher) 
        {
            if (gc.Wood > 150)
            {
                gc.Wood -= 150;
                BuildingOptions2.IsActive = true;
            }    
        } else if (BuildingOptions2.Type == Building.BuildingType.Church)
        {
            if (gc.Wood > 500)
            {
                gc.Wood -= 500;
                BuildingOptions2.IsActive = true;
            }
        }
    }
    
    public void SelectBuildOption3() {
        GameController gc = GameController.GetComponent<GameController>();
        if (BuildingOptions3.Type == Building.BuildingType.Farm) 
        {
            if (gc.Wood > 50)
            {
                gc.Wood -= 50;
                BuildingOptions3.IsActive = true;
            }    
        } else if (BuildingOptions3.Type == Building.BuildingType.Woodcutter)
        {
            if (gc.Wood > 80)
            {
                gc.Wood -= 80;
                BuildingOptions3.IsActive = true;
            }    
        } else if (BuildingOptions3.Type == Building.BuildingType.Fisher) 
        {
            if (gc.Wood > 150)
            {
                gc.Wood -= 150;
                BuildingOptions3.IsActive = true;
            }    
        } else if (BuildingOptions3.Type == Building.BuildingType.Church)
        {
            if (gc.Wood > 500)
            {
                gc.Wood -= 500;
                BuildingOptions3.IsActive = true;
            }
        }
    }

    public void Sacrifice()
    {
        if(GameController.FreeHumans > 0)
        {
            GameObject.Destroy(GameController.FreeHumansL[0].gameObject);
            GameController.UpdateHumans();
            SelectedBuilding.GetComponent<clickforhouseinfos>().updateInfo();
            GameController.Satisfaction += 100;
            GameController.Food += 5;
            if(GameController.SacrificePoints == 0)
            {
                GameObject.Find("Main Camera").GetComponent<SoundController>().PlayFirstBlood();
            }
            else if(GameController.SacrificePoints % 2 == 0)
            {
                GameObject.Find("Main Camera").GetComponent<SoundController>().PlayBiteDust();
            }
            else if (GameController.SacrificePoints % 2 == 1)
            {
                GameObject.Find("Main Camera").GetComponent<SoundController>().PlayFreshMeat();
            }
            GameController.SacrificePoints += 1;
        }
    
    }

    public void MakeHuman()
    {
        if(GameController.Food >= 100)
        {
            GameController.makeHuman();
            GameController.UpdateHumans();
            SelectedBuilding.GetComponent<clickforhouseinfos>().updateInfo();
            GameController.Food -= 100;
            GameController.Satisfaction -= 5;
        }
    }
}
