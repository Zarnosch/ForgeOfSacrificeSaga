﻿using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    // Some Stuff for the Timecicle
    public int Day, Week, Month, Year, Hour;
    public int GameSpeed;
    // timeHandling
    private float lastGameTime;
    private int lastDay, lastWeek, lastMonth, lastYear, lastHour;

    // Ressources
    public int Food;
    public int Satisfaction;
    public int HumanCount;
    public int Wood;
    public int SacrificePoints;

    // HumanControlls
    public int FreeHumans;
    public int WorkingHumans;
    public GameObject HumanPrefab;
    public List<GameObject> Humans = new List<GameObject>();
    public List<HumanController> WorkingHumansL = new List<HumanController>();
    public List<HumanController> FreeHumansL = new List<HumanController>();


    // Building Controlls
    public List<Building> Buildings;
    private ButtonController buttonController;

    // UiAcress
    UIController UIFoo;

    // Use this for initialization
    void Start () {
        // Get some Gamestuff
        UIFoo = GameObject.Find("UI").GetComponent<UIController>();
        buttonController = GameObject.Find("ButtonController").GetComponent<ButtonController>();
        buttonController.SelectedBuilding.GetComponent<clickforhouseinfos>().OnMouseDown();

        // Time Initialize
        Day = 1; Week = 1; Month = 1; Year = 1; Hour = 1;
        lastDay = 1; lastWeek = 1; lastMonth = 1; lastYear = 1; lastHour = 0;
        UIFoo.Day_ = Day; UIFoo.Hour_ = Hour; UIFoo.Week_ = Week; UIFoo.Month_ = Month; UIFoo.Year_ = Year; UIFoo.Sacrifices_ = SacrificePoints;
        lastGameTime = Time.time;
        // Ressource initialization
        Food = 0;
        Satisfaction = 100;
        Wood = 0;
        SacrificePoints = 0;

        HumanPrefab.GetComponent<HumanController>().SetNewTarget(GameObject.Find("MainBuilding").GetComponent<Building>());
        
        for (int i = 0; i < 3; i++)
        {
            makeHuman();
        }
	}
	
	// Update is called once per frame
	void Update () {
        // Update every frame
        /*
        FreeHumans = GetFreeHumans().Count;
        WorkingHumans = GetWorkingHumans().Count;
        HumanCount = Humans.Count;
        WorkingHumansL = GetWorkingHumans();
        FreeHumansL = GetFreeHumans();
        */
        UpdateHumans();
        if (FreeHumans + WorkingHumans != HumanCount)
        {
            UpdateHumans();
            Debug.Log("Free: " + FreeHumans + " Working: " + WorkingHumans + " all: " + HumanCount);
        }
        UIFoo.Hour_ = Hour;
        UIFoo.Zufriedenheit_ = Satisfaction;
        UIFoo.Sacrifices_ = SacrificePoints;
        // Some season handling
        lastHour = Hour;
        // 24 Seconds = 1 Day;
        Hour = (int)(Time.time * GameSpeed - lastGameTime * GameSpeed);
        lastDay = Day;
        lastWeek = Week;
        lastMonth = Month;
        lastYear = Year;
        if(!(Hour < 24))
        {
            lastGameTime = Time.time;
            Hour = 1;
            lastDay = Day;
            Day++;
        }
        if(Day > 7)
        {
            Day = 1;
            lastWeek = Week;
            Week++;
        }
        if (Week > 4)
        {
            Week = 1;
            lastMonth = Month;
            Month++;
        }
        if (Month > 12)
        {
            Month = 1;
            lastYear = Year;
            Year++;
        }

        // Code, which runs once per hour
        if(lastHour != Hour)
        {
            //Debug.Log("Hour change " + Hour);
            //UpdateHumans();
            HumanCount = Humans.Count;
            FreeHumans = FreeHumansL.Count;
            WorkingHumans = WorkingHumansL.Count;
            buttonController.SelectedBuilding.GetComponent<clickforhouseinfos>().updateInfo();
        }
        // Code, which runs once per day
        if (lastDay != Day)
        {
            //Debug.Log("Day change");
            Satisfaction -= HumanCount -3;
            foreach(Building building in Buildings)
            {
                if (building.IsActive)
                {
                    if(building.Type == Building.BuildingType.Farm)
                    {
                        Food += building.RessPerDay;
                    }
                    else if(building.Type == Building.BuildingType.Woodcutter)
                    {
                        Wood += building.RessPerDay;
                    }
                    else if (building.Type == Building.BuildingType.Forrest)
                    {
                        Wood += building.RessPerDay;
                    }
                    else if (building.Type == Building.BuildingType.Fruits)
                    {
                        Food += building.RessPerDay;
                    }
                }
            }
            UIFoo.Nahrung_ = Food;
            UIFoo.Holz_ = Wood;
            UIFoo.Day_ = Day;
        }
        // Code, which runs once per week
        if (lastWeek != Week)
        {
            UIFoo.Week_ = Week;
            Satisfaction -= 10;
            //Debug.Log("Week change");
        }
        // Code, which runs once per month
        if (lastMonth != Month)
        {
            UIFoo.Month_ = Month;
            //Debug.Log("Month change");
        }
        // Code, which runs once per year
        if (lastYear != Year)
        {
            UIFoo.Year_ = Year;
            //Debug.Log("Year change");
        }
        if(Satisfaction <= 0)
        {
            Application.LoadLevel("EndState");
        }
    }

    public void makeHuman()
    {
        GameObject Human = Instantiate(HumanPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        Humans.Add(Human);
        UpdateHumans();
    }

    public List<HumanController> GetWorkingHumans()
    {
        List<HumanController> temp = new List<HumanController>();
        foreach(GameObject human in Humans)
        {
            if (human != null)
            {
                if (human.GetComponent<HumanController>().IsWorking)
                {
                    temp.Add(human.GetComponent<HumanController>());
                }
            }
        }
        return temp;
    }

    public List<HumanController> GetFreeHumans()
    {
        List<HumanController> temp = new List<HumanController>();
        foreach (GameObject human in Humans)
        {
            if(human != null)
            {
                if (!human.GetComponent<HumanController>().IsWorking)
                {
                    temp.Add(human.GetComponent<HumanController>());
                }
            }
        }
        return temp;
    }

    public void UpdateHumans()
    {
        List<GameObject> temp = new List<GameObject>();
        foreach(GameObject human in Humans)
        {
            if(human != null){
                temp.Add(human);
            }
        }
        Humans = temp;
        FreeHumansL = GetFreeHumans();
        WorkingHumansL = GetWorkingHumans();
    }

}
