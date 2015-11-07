using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class clickforhouseinfos : MonoBehaviour {
    public GameObject PopupWindow;
	// Use this for initialization
	void OnMouseDown()
    {
        Debug.Log("kay bro");
        if(PopupWindow)
        {
            PopupWindow.SetActive(true);
        }
    }
    void Update()
    {
        
        
    }

   
}
