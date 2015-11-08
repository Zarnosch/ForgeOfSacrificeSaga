using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
    public enum BuildingType
    {
        Farm, Woodcutter, MainBuilding, Fisher, Church, Forrest, Fruits
    }
    public int MaxWorker;
    public int CurrentWorker;
    [HideInInspector]
    public int Productivity;
    public bool IsActive;
    public int RessPerDay;
    public BuildingType Type;
    public int Lvl;
	// Use this for initialization
	void Start () {
        switch (Type)
        {
            case BuildingType.Farm:
                Productivity = 2;
                break;
            case BuildingType.Forrest:
                Productivity = 1;
                break;
            case BuildingType.Woodcutter:
                Productivity = 2;
                break;
            case BuildingType.MainBuilding:
                Productivity = 0;
                break;
            case BuildingType.Fruits:
                Productivity = 1;
                break;
        }
        
        IsActive = true;
        CurrentWorker = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(CurrentWorker > MaxWorker)
        {
            CurrentWorker = MaxWorker;
        }
    RessPerDay = CurrentWorker * Productivity * Lvl;
    }
}
