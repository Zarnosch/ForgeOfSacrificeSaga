using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class HumanController : MonoBehaviour {
	
	public float visitedRange;
	[RangeAttribute(0.0f, 0.1f)]
	public float SpeedScale;
	public bool showGizmos = true;
	public float duempelOffset;
	
	[HideInInspector]
	public Building targetBuilding;
	[HideInInspector]
	public Building TargetBuilding 
	{
		get { return targetBuilding; }
		set { 
				targetBuilding = value;
				newTargetSet = true; 
			}
	}
	
	private bool newTargetSet;
	private float tick = 0;
	private Vector3 moveDirection = Vector3.zero;
	private List<Building> Buildings = new List<Building>();
	
	private bool visited = true;
	private Bezier movePath = new Bezier(Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero); 
	
	
	// Use this for initialization
	void Start () {
		Buildings = GameObject.Find("GameController").GetComponent<GameController>().Buildings;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentHumanPos = transform.position;
		tick += SpeedScale;
		
		if (!visited)
		{
			moveDirection = movePath.GetPointAtTime(Mathf.Lerp(0, 1, tick));
		}
		else 
		{ //calculate new spline
			Building closestBuilding = GetClosestBuilding(currentHumanPos);
		
			var randomOtherBuildings = from build in Buildings where build != closestBuilding && build != targetBuilding select build;
			Building rndBuilding1 = randomOtherBuildings.ElementAt(Random.Range(0, randomOtherBuildings.Count()));
			Building rndBuilding2 = randomOtherBuildings.ElementAt(Random.Range(0, randomOtherBuildings.Count()));
			
			movePath = new Bezier(currentHumanPos, rndBuilding1.transform.position, rndBuilding2.transform.position, targetBuilding.transform.position);
			visited = false;
		}
		
		if (Vector3.Distance(currentHumanPos, targetBuilding.transform.position) < visitedRange)
		{
			visited = true;
			newTargetSet = false;
			targetBuilding = Buildings[Random.Range(0, Buildings.Count())];
			tick = 0;
		}
		
		transform.position = moveDirection;
	}
	
	private Building GetClosestBuilding(Vector3 humanPos) {
		float closestBuildingDist = 10000.0f;
		Building closestBuilding = null;
		
		foreach (var building in Buildings)
		{
			float currDist = Vector3.Distance(building.transform.position, humanPos);
			if(currDist < closestBuildingDist) {
				closestBuildingDist = currDist; 
				closestBuilding = building;
			} 
		}
		return closestBuilding;
	}
	
	void OnDrawGizmos() {
		if (showGizmos)
		{
			Gizmos.color = Color.yellow;
			for (float i = 0; i < 1; i += 0.05f)
			{
				Gizmos.DrawSphere(movePath.GetPointAtTime(i), 0.05f);
			}	
		}
    }
}
