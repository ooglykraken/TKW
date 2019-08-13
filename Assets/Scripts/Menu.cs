using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	public GameObject deathScreen;
	
	public void StartGame(){
		Application.LoadLevel("Gameplay");
	}
	
	public void EndGame(){
		Application.Quit();
	}
	
	private static Menu instance = null;
	
	public static Menu Instance(){
		if(instance == null){
			instance = GameObject.Find("Menu").GetComponent<Menu>();
		}
		
		return instance;
	}
}
