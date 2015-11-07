using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class clickforhouseinfos : MonoBehaviour {
    public GameObject PopupWindow;
    private Text Buildingtype;
    private Text MaxWorker;
    private Text CurrentWorker;
	// Use this for initialization
	void OnMouseDown()
    {
        Buildingtype = GameObject.Find("BuildingType").GetComponent<Text>();
        MaxWorker = GameObject.Find("MaxWorker").GetComponent<Text>();
        CurrentWorker = GameObject.Find("CurrentWorker").GetComponent<Text>();
        Buildingtype.text = "Gebäudetyp: " + GetComponent<Building>().Type.ToString();
        MaxWorker.text = "Max Worker: " + GetComponent<Building>().MaxWorker;
        if(GetComponent<Building>().Type == Building.BuildingType.MainBuilding)
        {
            CurrentWorker.text = "Current Worker: " + GetComponent<Building>().CurrentWorker;
        }
        CurrentWorker.text = "Current Worker: " + GetComponent<Building>().CurrentWorker;
        //Debug.Log("kay bro");
        if (PopupWindow)
        {
            PopupWindow.SetActive(true);
        }
    }
    void Update()
    {
        
        
    }

   
}
