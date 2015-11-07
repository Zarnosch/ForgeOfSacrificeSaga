using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour {
    public GameObject popup;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void closeButton()
    {
        popup.SetActive(false);
    }
}
