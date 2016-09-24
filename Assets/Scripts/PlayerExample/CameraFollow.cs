using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Rigidbody2D target;
	public float speed = 5;
	public Vector2 velocityMod = new Vector2(0.1f,0.1f);
	public Vector2 velocityMin = new Vector2(0.1f,0.1f);
	public Vector2 velocityMax = new Vector2(0.1f,0.1f);

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position; // set offset to be original position (Should probably be adjustable)
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		float targetSpeed = Mathf.Clamp(target.velocity.magnitude,1,Mathf.Infinity); // Clamp speed 
		Vector3 targetVelocity = new Vector3(
			Mathf.Clamp(target.velocity.x*velocityMod.x*speed,-velocityMax.x,velocityMax.x),//Get target velocity, clamped and modified
			Mathf.Clamp(target.velocity.y*velocityMod.y*speed,-velocityMax.y,velocityMax.y)
		);
		if(Mathf.Abs(targetVelocity.x) < velocityMin.x){ // if velocity is less than min, don't apply
			targetVelocity.x = 0;
		}
		if(Mathf.Abs(targetVelocity.y) < velocityMin.y){
			targetVelocity.y = 0;
		}
		Vector3 targetPosition = target.position;// fetch target position as Vector3
		transform.position = Vector3.Lerp(transform.position,offset+targetPosition+targetVelocity,Time.fixedDeltaTime*targetSpeed); //lerp position
	}

}
