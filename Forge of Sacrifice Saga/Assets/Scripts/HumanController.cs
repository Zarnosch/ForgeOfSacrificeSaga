using UnityEngine;
using System.Collections.Generic;

public class HumanController : MonoBehaviour {
	
	public float attractionRange;
	public Rect movementArea;
	public int tickToMoveChange;
	[RangeAttribute(0, 1)]
	public float SpeedScale;
	private int tick = 0;
	private Vector3 moveDirection = Vector3.zero;
	private List<Building> Buildings = new List<Building>(); 
	
	// Use this for initialization
	void Start () {
		Buildings = GameObject.Find("GameController").GetComponent<GameController>().Buildings;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentHumanPos = transform.position;
		Building clostestBuilding = GetClosestBuilding(currentHumanPos);
		if (Vector3.Distance(currentHumanPos, clostestBuilding.transform.position) > attractionRange)
		{
			moveDirection = Vector3.Normalize(clostestBuilding.transform.position - currentHumanPos); 
		} else {
			tick++;
			if (tick > tickToMoveChange)
			{
				tick = 0;
				Vector3 randMovePos = new Vector3(Random.Range(movementArea.x, movementArea.x + movementArea.width),
												Random.Range(movementArea.y, movementArea.y + movementArea.height), 
												currentHumanPos.z);
				moveDirection = Vector3.Normalize(randMovePos - currentHumanPos);
			}
		}
		
		transform.position += moveDirection * SpeedScale;
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
}
