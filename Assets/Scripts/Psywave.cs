using UnityEngine;
using System.Collections;

public class Psywave : MonoBehaviour {
	
	private int lifetime = 300;
	
	public void Awake(){
		
	}
	
	public void Update(){
		if(lifetime > 0){
			lifetime--;
		} else {
			Destroy(this.gameObject);
		}
	}
	
	public void OnTriggerEnter(Collider c){
		if(c.tag == "Enemy"){
			Gameplay.Instance().AddKill();
			
			Destroy(c.gameObject);
			Destroy(this.gameObject);
		}
	}
}
