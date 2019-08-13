using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	private float verticalInput;
	private float horizontalInput;
	
	private float movementSpeed = 8f;
	
	private float gravity = -10f;
	
	private float timeModifier = 6f;
	
	private int jumpTimer;
	private int jumpTime = 15;
	
	private int shotTimer;
	private int shotCooldown = 15;
	
	private bool grounded;
	public bool isJumping;
	
	private Collider thisCollider;
	private SpriteRenderer thisRenderer;
	private Rigidbody thisRigidbody;
	
	public void Awake(){
		verticalInput = 0;
		horizontalInput = 0;
		
		grounded = false;
		isJumping = false;
		
		thisCollider = transform.Find("Collider").gameObject.GetComponent<Collider>();
		thisRenderer = transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>();
		thisRigidbody = this.gameObject.GetComponent<Rigidbody>();
	}
	
	public void Update(){
		// verticalInput = Input.GetAxis("Vertical");
		// horizontalInput = Input.GetAxis("Horizontal");
		 
		// if(horizontalInput == -1f){
			// thisRenderer.flipX = true;
		// } else if(horizontalInput == 1f) {
			// thisRenderer.flipX = false;
		// } 
		
		verticalInput = Input.GetAxis("Vertical");
		horizontalInput = Input.GetAxis("Horizontal");
		 
		if(horizontalInput < 0f){
			thisRenderer.flipX = true;
		} else if(horizontalInput > 0f) {
			thisRenderer.flipX = false;
		} 
		
		if(isJumping){
			jumpTimer--;
			if(jumpTimer <= 0){
				isJumping = false;
			}
		} 
		
		if(shotTimer > 0){
			shotTimer--;
		}
		 
		if(Input.GetKeyDown("space") && shotTimer <= 0){
			if(!grounded){
				FireDown();
			} else{
				Fire();
			}
		}
		
		
	}
	
	public void FixedUpdate(){
		
		
		
		// if()
		
		grounded = Grounded();
		// Debug.Log(grounded);
		
		if(!grounded && !isJumping){
			Gravity();
			return;
		} 
		
		if(Input.GetKeyDown("w") && !isJumping){
			Jump();
		}
		
		if(horizontalInput != 0f){
			Move();
		} else {
			Inertia();
		}
	}
	
	private void Move(){
		
		
		Vector3 newVelocity = new Vector3(movementSpeed * horizontalInput, thisRigidbody.velocity.y, 0f);
		
		thisRigidbody.velocity = Vector3.Lerp(thisRigidbody.velocity, newVelocity, Time.deltaTime * timeModifier);
	}
	
	private void Gravity(){
		Vector3 newVelocity = new Vector3(thisRigidbody.velocity.x, gravity, 0f);
		
		thisRigidbody.velocity = Vector3.Lerp(thisRigidbody.velocity, newVelocity, Time.deltaTime * timeModifier);
	}
	
	private void Inertia(){
		Vector3 newVelocity = new Vector3(0f, thisRigidbody.velocity.y , 0f);
		
		thisRigidbody.velocity = Vector3.Lerp(thisRigidbody.velocity, newVelocity, Time.deltaTime * timeModifier);
	}
	
	private bool Grounded(){
		float distance = 2f;

		RaycastHit hit;
		
		Vector3 ray = new Vector3(thisCollider.transform.position.x, thisCollider.transform.position.y, 0f);
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
	
	private void Jump(){
		// Debug.Log("Jumping");
		
		isJumping = true;
		
		// Vector3 newVelocity = new Vector3(movementSpeed * horizontalInput, -8f * gravity, 0f);
		
		// thisRigidbody.velocity = Vector3.Lerp(thisRigidbody.velocity, newVelocity, Time.deltaTime * timeModifier);
		
		thisRigidbody.AddForce(transform.up * .00007f);
		
		jumpTimer = jumpTime;
	}
	
	private void Fire(){						
		shotTimer = shotCooldown;
		
		float horizontalDirection = horizontalInput;
		
		GameObject projectile = (Instantiate(Resources.Load("Projectile", typeof(GameObject))) as GameObject) as GameObject;
		
		if(horizontalDirection == 0f){
			horizontalDirection = 1f;
		}
		
		projectile.transform.position = thisCollider.transform.position + (new Vector3(3f, 0f, 0f) * horizontalDirection);
		projectile.transform.parent = transform;
		
		// Debug.Log(horizontalInput);
		
		if(horizontalDirection < 0f){
			projectile.GetComponent<SpriteRenderer>().flipX = true;
		}
		
		Vector3 projectileVelocity =  new Vector3(300f, 0f, 0f) * horizontalDirection;
		
		projectile.GetComponent<Rigidbody>().velocity = Vector3.Lerp(Vector3.zero, projectileVelocity, Time.deltaTime * timeModifier);
		
		// gameObject.GetComponent<AudioSource>().Play();
	}
	
	private void FireDown(){						
		shotTimer = shotCooldown;
				
		GameObject projectile = (Instantiate(Resources.Load("Projectile", typeof(GameObject))) as GameObject) as GameObject;
		
		projectile.transform.position = thisCollider.transform.position + new Vector3(0f, -1f, 0f);
		projectile.transform.parent = transform;
				
		projectile.transform.eulerAngles = new Vector3(0f, 0f, -90f);
		
		Vector3 projectileVelocity =  new Vector3(0f, -250f, 0f);
		
		projectile.GetComponent<Rigidbody>().velocity = Vector3.Lerp(Vector3.zero, projectileVelocity, Time.deltaTime * timeModifier);
		
		
		thisRigidbody.AddForce(transform.up * .00005f);
		// gameObject.GetComponent<AudioSource>().Play();
	}
	
	// private void CameraShift(){
		// float cameraShift = 9f;
		// shifting = true;
		
		// Transform target = Camera.main.transform;
		
		// target.position = new Vector3(transform.localPosition.x + cameraShift * direction ,transform.localPosition.y + cameraShift * Input.GetAxisRaw("Vertical"), target.position.z);
	// }
	
	public void Death(){
		Debug.Log("Player died");
		Menu.Instance().transform.Find("DeathScreen").gameObject.SetActive(true);
		Menu.Instance().transform.Find("DeathScreen/txtKills").gameObject.GetComponent<TextMesh>().text = "Kills: " + Gameplay.Instance().kills;
		Gameplay.Instance().gameObject.SetActive(false);
	}
	
	public void LateUpdate(){
		CameraFollow();
	}
	
	private void CameraFollow(){
		float cameraOffset = 2.4f;
		float verticalOffset = 1f;
		Camera main = Camera.main;
		
		if(transform.position.x  >  main.transform.position.x + cameraOffset){
			main.transform.position = new Vector3( transform.position.x - cameraOffset, main.transform.position.y, main.transform.position.z);
		} else if(transform.position.x  <  main.transform.position.x - cameraOffset){
			main.transform.position = new Vector3( transform.position.x + cameraOffset, main.transform.position.y, main.transform.position.z);
		}
		// if(transform.position.y  >  main.transform.position.y + verticalOffset){
			// main.transform.position = new Vector3(main.transform.position.x, transform.position.y - verticalOffset, main.transform.position.z);
		// } else if(transform.position.y  <  main.transform.position.y - verticalOffset){
			// main.transform.position = new Vector3(main.transform.position.x, transform.position.y + verticalOffset, main.transform.position.z);
		// }
	}
	
	private static Player instance = null;
	
	public static Player Instance(){
		if(instance == null){
			instance = GameObject.Find("Player").GetComponent<Player>();
		}
		
		return instance;
	}
}
