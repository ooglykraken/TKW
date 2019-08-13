using UnityEngine;
using System.Collections;

public class PenguinEnemy : MonoBehaviour {
	
	private float directionModifier;
	private float movementSpeed;
	private bool grounded;
	
	public void Awake(){
		movementSpeed = Random.Range(.3f, 1.5f);
		float sizeModifier = Random.Range(.6f, 1.4f);
		
		transform.localScale *= sizeModifier;
	}
	
	public void Update(){
		if(grounded){
			Move();
		}
	}
	
	public void FixedUpdate(){
		grounded = Grounded();
		
		if(!grounded){
			Gravity();
		}
	}
	
	private void Move(){
		if(transform.position.x > Player.Instance().transform.position.x){
			directionModifier = -1f;
		} else {
			directionModifier = 1f;
		}
		
		transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(directionModifier * movementSpeed, 0f, 0f), Time.deltaTime * 4f);
	}
	
	public void OnCollisionEnter(Collision c){
		Debug.Log(c.transform.tag);
		
		if(c.transform.parent != null){
			if(c.transform.parent.tag == "Player"){
				Destroy(c.transform.parent.gameObject);
			}else if(c.transform.tag == "Player"){
				Player.Instance().Death();
			}
		}else if(c.transform.tag == "Player"){
			Player.Instance().Death();
		}
	}
	
	private void Gravity(){
		// Vector3 newVelocity = new Vector3(thisRigidbody.velocity.x, gravity, 0f);
		
		transform.position = Vector3.Lerp(transform.position, transform.position - new Vector3(0f, 4f, 0f), Time.deltaTime * 4f);
	}
	
	// private void Inertia(){
		// Vector3 newVelocity = new Vector3(0f, thisRigidbody.velocity.y , 0f);
		
		// thisRigidbody.velocity = Vector3.Lerp(thisRigidbody.velocity, newVelocity, Time.deltaTime * timeModifier);
	// }
	
	private bool Grounded(){
		float distance = 1f;

		RaycastHit hit;
		
		Vector3 ray = new Vector3(transform.position.x, transform.position.y, 0f);
		if (Physics.Raycast(ray, -transform.up, out hit, distance)) {
			// if (Mathf.Abs(Vector3.Angle(hit.normal, transform.up)) <= angleAllowance) {
				// if (Vector3.Distance(thisCollider.transform.position, hit.point) <= distance){

					// hitPoint = centerHit.point;
					return true;
				// }
			// }
			// return false;
		}
		
		return false;
	}
}
