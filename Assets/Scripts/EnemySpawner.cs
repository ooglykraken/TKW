using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	
	private int timer;
	private int timeToSpawn = 100;
	
	public void Update(){
		if(timer > 0f){
			timer--;
		} else {
			Spawn();
		}
	}
	
	private void Spawn(){
		GameObject penguin = Instantiate(Resources.Load("Penguin", typeof(GameObject)) as GameObject) as GameObject;
		
		penguin.transform.position = transform.position;
		timer = timeToSpawn + Random.Range(-15, 15);
	}
}
