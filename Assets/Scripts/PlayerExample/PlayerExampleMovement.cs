using UnityEngine;
using System.Collections;

/// <summary>
/// Example class for player movement.
/// </summary>


[RequireComponent(typeof(Rigidbody2D))] // Only use on stuff with Rigidbody2D
public class PlayerExampleMovement : MonoBehaviour {

	public float acceleration = 100; // Values for acceleration and speed
	public float topSpeed = 1000;

	private Rigidbody2D playerRigidbody;
	private float xInput = 0;


	// Use this for initialization
	void Start () {
		playerRigidbody = GetComponent<Rigidbody2D>(); // Grab dat rigidbody
	}
	
	// Update is called once per frame
	void Update () {
		xInput = Input.GetAxis("Horizontal"); // Get player input (normally want to use an InputManager or something)
	}

	void FixedUpdate(){
		if(Mathf.Abs(playerRigidbody.angularVelocity) < topSpeed){ //If under the topspeed (rotational speed, not actual velocity!)
			playerRigidbody.AddTorque(acceleration*-xInput*Time.fixedDeltaTime); // add rotational velocity
		}
	}

}
