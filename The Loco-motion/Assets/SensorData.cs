using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccelControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Input.gyro.enabled = true;
		Screen.orientation = ScreenOrientation.Portrait;
	}

	public Text accelData;
	public Text stepCount;

	private int eventCnt = 0;
	private bool currentlyMoving = false;
	private int sampleCnt = 0;

	// Update is called once per frame
	void FixedUpdate () {
		Vector3 dir = Input.acceleration;
		Quaternion orientation = Input.gyro.attitude;
		Vector3 adjustedOrientation = orientation * dir;
		Vector3 adjustedForGravity = new Vector3 (adjustedOrientation.x, adjustedOrientation.y, adjustedOrientation.z + 1);
		accelData.text = adjustedForGravity.ToString ();

		if (adjustedForGravity.magnitude > 0.6) {
			sampleCnt++;
			if (!currentlyMoving && sampleCnt > 5) {
				currentlyMoving = true;
				eventCnt++;
			}
		} else {
			sampleCnt = 0;
			currentlyMoving = false;
		}

		stepCount.text = eventCnt.ToString ();
	}

	private void logStep() {
		eventCnt++;
	}

}
