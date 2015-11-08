using UnityEngine;
using System.Collections;

public class BackToMenu : MonoBehaviour {
	
	public void ToMenu() {
		Application.LoadLevel("MainMenuState");
	}
	
}
