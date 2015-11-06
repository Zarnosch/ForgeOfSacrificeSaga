using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    // Some Stuff for the Timecicle
    public int Day, Week, Month, Year, Hour;
    public int GameSpeed;
    // timeHandling
    private float lastGameTime;
    private int lastDay, lastWeek, lastMonth, lastYear, lastHour;

    // Use this for initialization
    void Start () {
        Day = 1; Week = 1; Month = 1; Year = 1; Hour = 1;
        lastDay = 1; lastWeek = 1; lastMonth = 1; lastYear = 1; lastHour = 0;
        lastGameTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
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
        }
        // Code, which runs once per day
        if (lastDay != Day)
        {
            //Debug.Log("Day change");
        }
        // Code, which runs once per week
        if (lastWeek != Week)
        {
            //Debug.Log("Week change");
        }
        // Code, which runs once per month
        if (lastMonth != Month)
        {
            //Debug.Log("Month change");
        }
        // Code, which runs once per year
        if (lastYear != Year)
        {
            //Debug.Log("Year change");
        }
    }
}
