using UnityEngine;
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
    public GameObject HumanPrefab;
    public List<GameObject> Humans = new List<GameObject>();


    // Building Controlls
    public List<Building> Buildings;

    // UiAcress
    UIController UIFoo;

    // Use this for initialization
    void Start () {
        // Get some Gamestuff
        UIFoo = GameObject.Find("UI").GetComponent<UIController>();

        // Time Initialize
        Day = 1; Week = 1; Month = 1; Year = 1; Hour = 1;
        lastDay = 1; lastWeek = 1; lastMonth = 1; lastYear = 1; lastHour = 0;
        UIFoo.Day_ = Day; UIFoo.Hour_ = Hour; UIFoo.Week_ = Week; UIFoo.Month_ = Month; UIFoo.Year_ = Year;
        lastGameTime = Time.time;
        // Ressource initialization
        Food = 0;
        Satisfaction = 100;
        Wood = 0;
        SacrificePoints = 0;
        
        HumanPrefab.GetComponent<HumanController>().targetBuilding = Buildings[0];
        for (int i = 0; i < 10; i++)
        {
            makeHuman();
        }        
	}
	
	// Update is called once per frame
	void Update () {
        HumanCount = Humans.Count;
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
            UIFoo.Hour_ = Hour;
            UIFoo.Zufriedenheit_ = Satisfaction;
        }
        // Code, which runs once per day
        if (lastDay != Day)
        {
            //Debug.Log("Day change");
            Satisfaction -= 2;
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
            Debug.Log("You lose!");
        }
    }

    public void makeHuman()
    {
        GameObject Human = Instantiate(HumanPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        Humans.Add(Human);
    }
}
