using UnityEngine;
using System.Collections;

public class FarmController : MonoBehaviour {
    public int MaxWorker;
    public int CurrentWorker;
    public int Productivity;
    public bool IsActive;
    public int foodPerDay;
	// Use this for initialization
	void Start () {

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
