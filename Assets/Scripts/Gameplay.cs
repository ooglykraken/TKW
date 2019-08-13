using UnityEngine;
using System.Collections;

public class Gameplay : MonoBehaviour {
	
	public int kills;
	
	public GameObject counterObject;
	
	private TextMesh killCounter;
	
	public void Awake(){
		killCounter = counterObject.GetComponent<TextMesh>();
		
		kills = 0;
		
		UpdateCounter();
	}
	
	public void AddKill(){
		kills++;
		
		UpdateCounter();
	}
	
	private void UpdateCounter(){
		killCounter.text = "Kills: " + kills.ToString();
	}
	
	private static Gameplay instance = null;
	
	public static Gameplay Instance(){
		if(instance == null){
			instance = GameObject.Find("Gameplay").GetComponent<Gameplay>();
			
			
		}
		
		return instance;
	}
}
