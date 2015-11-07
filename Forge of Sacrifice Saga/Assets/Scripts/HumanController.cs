using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class HumanController : MonoBehaviour {
	
	public float visitedRange;
	[RangeAttribute(0.0f, 0.1f)]
	public float SpeedScale;
	public bool showGizmos = true;
	public float duempelOffset;
	public float duempelTimerMax;
	
	[HideInInspector]
	public Building targetBuilding;
	[HideInInspector]
	public bool newTargetSet;
	
	private float tick = 0;
	private float duempelTimer = 0;
	private bool duempeln = false;
	private Vector3 moveDirection = Vector3.zero;
	private List<Building> Buildings = new List<Building>();
	
	private bool calculateMovement = false;
	private bool reachedTarget = false;
	private Bezier movePath = new Bezier(Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero); 
	
	
	// Use this for initialization
	void Start () {
		Buildings = GameObject.Find("GameController").GetComponent<GameController>().Buildings;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentHumanPos = transform.position;
		tick += SpeedScale;
		
		if (calculateMovement) {
			moveDirection = movePath.GetPointAtTime(Mathf.Lerp(0, 1, tick));
		} else if (!reachedTarget && !calculateMovement)
		{
			Debug.Log("new target");
			Building closestBuilding = GetClosestBuilding(currentHumanPos);
		
			var randomOtherBuildings = from build in Buildings where build != closestBuilding && build != targetBuilding select build;
			Building rndBuilding1 = randomOtherBuildings.ElementAt(Random.Range(0, randomOtherBuildings.Count()));
			Building rndBuilding2 = randomOtherBuildings.ElementAt(Random.Range(0, randomOtherBuildings.Count()));
			
			movePath = new Bezier(currentHumanPos, rndBuilding1.transform.position, rndBuilding2.transform.position, targetBuilding.transform.position);
			calculateMovement = true;
		} else
		{
			Debug.Log("duempeln");
			Vector3 targetPos = targetBuilding.transform.position;
			Rect duempelArea = new Rect(targetPos.x - duempelOffset, targetPos.y - duempelOffset, duempelOffset*2, duempelOffset*2);
			movePath = new Bezier(currentHumanPos, 
								new Vector3(duempelArea.x, duempelArea.y, currentHumanPos.z),
								new Vector3(duempelArea.x, duempelArea.y + duempelArea.height, currentHumanPos.z),
								new Vector3(duempelArea.x + duempelArea.width, duempelArea.y, currentHumanPos.z));
			calculateMovement = true;
			duempeln = true;
		}
		
		if (duempeln)
		{
			duempelTimer++;
		}
		if (duempeln && (duempelTimer > duempelTimerMax))
		{
			Debug.Log("new duempel target");
			duempelTimer = 0;
			tick = 0;
			
			calculateMovement = false;
		}
		
		if ((Vector3.Distance(currentHumanPos, targetBuilding.transform.position) < visitedRange) && newTargetSet)
		{
			tick = 0;
			
			reachedTarget = true;
			duempeln = true;
			newTargetSet = false;
			calculateMovement = false;
		}
		
		transform.position = moveDirection;
	}
	
	public void SetNewTarget() {
		targetBuilding = Buildings[Random.Range(0, Buildings.Count())];
		reachedTarget = false;
		duempeln = false;
		newTargetSet = true;	
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
