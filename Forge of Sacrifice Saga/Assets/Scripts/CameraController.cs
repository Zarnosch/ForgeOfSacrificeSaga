using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float cameraMoveSpeed;
    public float moveOffset;

    private Rect cameraBounds;
    float camHeight;
    float camWidth;

    private SpriteRenderer map;

    // Use this for initialization
    void Start() {
        cameraBounds.x = moveOffset;
        cameraBounds.width = Screen.width - (moveOffset * 2);
        cameraBounds.y = moveOffset;
        cameraBounds.height = Screen.height - (moveOffset * 2);
        //map = GameObject.Find("Map").GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        //cameraBounds.center = Camera.main.transform.position;
        //Debug.Log(cameraBounds.x);
		Vector2 mousePos = Input.mousePosition;
		if (mousePos.x < cameraBounds.x && Camera.main.transform.position.x > - 8.5f) 
		{
			//Debug.Log("move left");
			Camera.main.transform.position -= new Vector3(cameraMoveSpeed, 0, 0);
		}
		if (mousePos.x > (cameraBounds.x + cameraBounds.width) && Camera.main.transform.position.x < 8.5f) 
		{
			//Debug.Log("move right");
			Camera.main.transform.position += new Vector3(cameraMoveSpeed, 0, 0);
		}
		if (mousePos.y < cameraBounds.y && Camera.main.transform.position.y > -5.5f) 
		{
			//Debug.Log("move up");
			Camera.main.transform.position -= new Vector3(0, cameraMoveSpeed, 0);
		}
		if (mousePos.y > (cameraBounds.y + cameraBounds.height) && Camera.main.transform.position.y < 6f) 
		{
			//Debug.Log("move down");
			Camera.main.transform.position += new Vector3(0, cameraMoveSpeed, 0);
		}
	}
}
