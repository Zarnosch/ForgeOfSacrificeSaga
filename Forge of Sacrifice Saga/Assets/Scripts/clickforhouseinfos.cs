using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class clickforhouseinfos : MonoBehaviour {
    public GameObject PopupWindow;
    private GameController GameController;
    private Text Buildingtype;
    private Text MaxWorker;
    private Text CurrentWorker;
    private Text FreeWorker;
	// Use this for initialization
    void start()
    {
        Buildingtype = GameObject.Find("BuildingType").GetComponent<Text>();
        MaxWorker = GameObject.Find("MaxWorker").GetComponent<Text>();
        GameController = GameObject.Find("GameController").GetComponent<GameController>();
        CurrentWorker = GameObject.Find("CurrentWorker").GetComponent<Text>();
        FreeWorker = GameObject.Find("FreeWorker").GetComponent<Text>();
        GameObject.Find("ButtonController").GetComponent<ButtonController>().SelectedBuilding = gameObject;
        Buildingtype.text = "Gebäudetyp: " + GetComponent<Building>().Type.ToString();
        MaxWorker.text = "Max Arbeiter: " + GetComponent<Building>().MaxWorker;
        FreeWorker.text = "Freie Arbeiter: " + GameController.FreeHumans;
        if (GetComponent<Building>().Type == Building.BuildingType.MainBuilding)
        {
            CurrentWorker.text = "Arbeiter gesammt: " + (GameController.Humans.Count - GameController.FreeHumans);
        }
        else
        {
            CurrentWorker.text = "Arbeiter: " + GetComponent<Building>().CurrentWorker;
        }
        if (PopupWindow)
        {
            PopupWindow.SetActive(true);
        }
    }    
	public void OnMouseDown()
    {
        Buildingtype = GameObject.Find("BuildingType").GetComponent<Text>();
        MaxWorker = GameObject.Find("MaxWorker").GetComponent<Text>();
        GameController = GameObject.Find("GameController").GetComponent<GameController>();
        CurrentWorker = GameObject.Find("CurrentWorker").GetComponent<Text>();
        FreeWorker = GameObject.Find("FreeWorker").GetComponent<Text>();        
        GameObject.Find("ButtonController").GetComponent<ButtonController>().SelectedBuilding = gameObject;
        updateInfo();
    }
    void Update()   {

    }

    public void updateInfo()
    {
        Buildingtype.text = "Gebäudetyp: " + GetComponent<Building>().Type.ToString();    
        MaxWorker.text = "Max Arbeiter: " + GetComponent<Building>().MaxWorker;
        FreeWorker.text = "Freie Arbeiter: " + GameController.GetFreeHumans().Count;
        if (GetComponent<Building>().Type == Building.BuildingType.MainBuilding)
        {
            CurrentWorker.text = "Arbeiter gesammt: " + (GameController.HumanCount - GameController.GetFreeHumans().Count);
        }
        else
        {
            CurrentWorker.text = "Arbeiter: " + GetComponent<Building>().CurrentWorker;
        }
        if (PopupWindow)
        {
            PopupWindow.SetActive(true);
        }
    }


}
