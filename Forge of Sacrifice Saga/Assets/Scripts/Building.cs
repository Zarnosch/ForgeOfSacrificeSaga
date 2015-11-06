using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
    public enum BuildingType
    {
        Farm, Woodcutter, MainBuilding
    }
    public int MaxWorker;
    public int CurrentWorker;
    public int Productivity;
    public bool IsActive;
    public int foodPerDay;
    public BuildingType Type;
	// Use this for initialization
	void Start () {
        Productivity = 0;
        IsActive = false;
        CurrentWorker = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(CurrentWorker > MaxWorker)
        {
            CurrentWorker = MaxWorker;
        }

        foodPerDay = CurrentWorker * Productivity;
    }
}
