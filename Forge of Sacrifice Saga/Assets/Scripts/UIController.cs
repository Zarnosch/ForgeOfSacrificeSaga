using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    private Text Zufriedenheit;
    private Text Nahrung;
    private Text Holz;
    private Text Hour;
    private Text Day;
    private Text Week;
    private Text Month;
    private Text Year;
    public int Zufriedenheit_;
    public int Nahrung_;
    public int Holz_;
    public int Hour_;
    public int Day_;
    public int Week_;
    public int Month_;
    public int Year_;

    // Use this for initialization
    void Start () {
        Zufriedenheit = GameObject.Find("Zufriedenheit").GetComponent<Text>();
        Nahrung = GameObject.Find("Nahrung").GetComponent<Text>();
        Holz = GameObject.Find("Holz").GetComponent<Text>();
        Hour = GameObject.Find("Stunde").GetComponent<Text>();
        Day = GameObject.Find("Tag").GetComponent<Text>();
        Week = GameObject.Find("Woche").GetComponent<Text>();
        Month = GameObject.Find("Monat").GetComponent<Text>();
        Year = GameObject.Find("Jahr").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        Zufriedenheit.text = "Zufriedenheit: " + Zufriedenheit_;
        Nahrung.text = "Nahrung: " + Nahrung_;
        Holz.text = "Holz: " + Holz_;
        Hour.text = "Stunde: " + Hour_;
        Day.text = "Tag: " + Day_;
        Week.text = "Woche: " + Week_;
        Month.text = "Monat: " + Month_;
        Year.text = "Jahr: " + Year_;

    }
}
