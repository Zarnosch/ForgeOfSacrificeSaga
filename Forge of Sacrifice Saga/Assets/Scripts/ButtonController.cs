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
        SelectedBuilding.GetComponent<clickforhouseinfos>().updateInfo();

    }
    public void RemoveWorker()
    {
        if (SelectedBuilding.GetComponent<Building>().CurrentWorker > 0)
        {
            SelectedBuilding.GetComponent<Building>().CurrentWorker -= 1;
            //foreach()
            //GameController.WorkingHumans
            //GameController.FreeHumansL[0].SetNewTarget(SelectedBuilding.GetComponent<Building>());
        }
        SelectedBuilding.GetComponent<clickforhouseinfos>().updateInfo();

    }
}
