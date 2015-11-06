using UnityEngine;
using System.Collections.Generic;

public class HumanController : MonoBehaviour {
	
	public List<GameObject> Buildings = new List<GameObject>();
	public float attractionRange;
	public Rect movementArea;
	public int tickToMoveChange;
	[RangeAttribute(0, 1)]
	public float SpeedScale;
	
	public GameObject debugTarget;
	public GameObject testBuildingObject;
	
	private int tick = 0;
	private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
		Buildings.Add(testBuildingObject);	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentHumanPos = transform.position;
		GameObject clostestBuilding = GetClosestBuilding(currentHumanPos);
		if (Vector3.Distance(currentHumanPos, clostestBuilding.transform.position) > attractionRange)
		{
			moveDirection = Vector3.Normalize(clostestBuilding.transform.position - currentHumanPos);
			debugTarget.transform.position = clostestBuilding.transform.position; 
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
	
	private GameObject GetClosestBuilding(Vector3 humanPos) {
		float closestBuildingDist = 10000.0f;
		GameObject closestBuilding = null;
		
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
