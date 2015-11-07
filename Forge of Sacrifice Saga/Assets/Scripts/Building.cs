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
    public int RessPerDay;
    public BuildingType Type;
	// Use this for initialization
	void Start () {
        Productivity = 1;
        IsActive = true;
        CurrentWorker = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(CurrentWorker > MaxWorker)
        {
            CurrentWorker = MaxWorker;
        }
        RessPerDay = CurrentWorker * Productivity;
    }
}
