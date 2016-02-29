using UnityEngine;
using System.Collections;

public class ArrowControllerScript : MonoBehaviour {


	//The arrow constantly rotates around the ship while in running mode
	//The arrow draws back (like on a bowstring) while in stop mode
	//The arrow flies forward when in rebound mode
	private enum State { STOP, REBOUND, RUNNING };

	//How fast the arrow should spin around the ship
	public float Speed = 50;

	//Current state of the arrow
	private State currentState = State.RUNNING;

	//A link to the arrow. You could probably also do this by finding a child of the gameObject's transform, but this works too.
	public GameObject arrow;

	//Where the arrow starts its rebound
	private float reboundStartY;

	private float reboundEndY;

	//Length of time to perform a rebound
	const float REBOUND_TIME = 0.2f;

	//Amount of time left before the current rebound is over
	float currentReboundTime = REBOUND_TIME;

	float power;

	void Start() {
		//Note: We're using the arrow's local position (to this object) rather than its global position
		reboundEndY = arrow.transform.localPosition.y;
	}

	// Update is called once per frame
	void Update () {
		if (currentState == State.RUNNING) {
			//Slowly rotating the arrow around the z axis, we rotate the current transform and it rotates the child
			//in local space
			Vector3 myRotationAsEulers = this.transform.rotation.eulerAngles;
			myRotationAsEulers.z += (Time.deltaTime * Speed);
			Quaternion newRotation = Quaternion.identity;
			newRotation.eulerAngles = myRotationAsEulers;
			this.transform.rotation = newRotation;
		} else if (currentState == State.STOP) {
			//arrow.gameObject.SetActive(false);
			//While we're stopped move the arrow's position back
			Vector3 arrowPosition = arrow.transform.localPosition;
			arrowPosition.y += Time.deltaTime;
			reboundStartY = arrowPosition.y;
			arrow.transform.localPosition = arrowPosition;
		} else if (currentState == State.REBOUND) {
			//arrow.gameObject.SetActive(true);
			//Slowly take time off the rebound timer...
			currentReboundTime -= Time.deltaTime;
			Vector3 arrowPosition = arrow.transform.localPosition;
			//Which linearly interpolates between where the arrow started and its original location
			float newY = Mathf.Lerp (reboundStartY, reboundEndY, 1f - currentReboundTime / REBOUND_TIME);
			arrowPosition.y = newY;
			arrow.transform.localPosition = arrowPosition;
			//If we're done rebounding, go back to rotating
			if (currentReboundTime <= 0.0f) {
				currentState = State.RUNNING;
				currentReboundTime = REBOUND_TIME;
			}
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			currentState = State.STOP;
		} else if (Input.GetKeyUp(KeyCode.A) && !(currentState == State.REBOUND)) {
			currentState = State.REBOUND;
		}
	
	}
}
