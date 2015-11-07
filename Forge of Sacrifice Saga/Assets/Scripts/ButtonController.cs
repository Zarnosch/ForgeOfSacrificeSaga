using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour {
    public GameObject popup;
    [HideInInspector]
    public GameObject SelectedBuilding;
    public GameController GameController;
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

    public void Sacrifice()
    {
        if(GameController.FreeHumans > 0)
        {
            GameObject.Destroy(GameController.FreeHumansL[0].gameObject);
        }
        GameController.UpdateHumans();
        SelectedBuilding.GetComponent<clickforhouseinfos>().updateInfo();
        GameController.Satisfaction += 100;
        GameController.Food += 5;      
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
