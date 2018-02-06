using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	const int STEERING_MODE_SEEK = 0;
	const int STEERING_MODE_FLEE = 1;
	const int STEERING_MODE_ARRIVAL = 2;
	const int STEERING_MODE_WANDER = 3;
	const int STEERING_MODE_COLLISION_AVOIDANCE = 4;
	const int STEERING_MODE_LEADER_FOLLOWING = 5;
	const int STEERING_MODE_QUEUE = 6;

	public void OnSteeringModeValueChanged (Dropdown obj) {
		switch (obj.value) {
		case STEERING_MODE_SEEK:
			SceneManager.LoadScene ("Seek");
			break;
		case STEERING_MODE_FLEE:
			SceneManager.LoadScene ("Flee");
			break;
		case STEERING_MODE_ARRIVAL:
			SceneManager.LoadScene ("Arrival");
			break;
		case STEERING_MODE_WANDER:
			SceneManager.LoadScene ("Wander");
			break;
		case STEERING_MODE_COLLISION_AVOIDANCE:
			SceneManager.LoadScene ("Collision Avoidance_Seek");
			break;
		case STEERING_MODE_LEADER_FOLLOWING:
			SceneManager.LoadScene ("Leader Following_Seek");
			break;
		case STEERING_MODE_QUEUE:
			SceneManager.LoadScene ("Queue");
			break;
		}
	}

	public void OnCollisionAvoidanceMovementTypeValueChanged (Dropdown obj) {
		switch (obj.value) {
		case STEERING_MODE_SEEK:
			SceneManager.LoadScene ("Collision Avoidance_Seek");
			break;
		case STEERING_MODE_FLEE:
			SceneManager.LoadScene ("Collision Avoidance_Flee");
			break;
		case STEERING_MODE_ARRIVAL:
			SceneManager.LoadScene ("Collision Avoidance_Arrival");
			break;
		case STEERING_MODE_WANDER:
			SceneManager.LoadScene ("Collision Avoidance_Wander");
			break;
		}
	}

	public void OnLeaderFollowingMovementTypeValueChanged (Dropdown obj) {
		switch (obj.value) {
		case STEERING_MODE_SEEK:
			SceneManager.LoadScene ("Leader Following_Seek");
			break;
		case STEERING_MODE_FLEE:
			SceneManager.LoadScene ("Leader Following_Flee");
			break;
		case STEERING_MODE_ARRIVAL:
			SceneManager.LoadScene ("Leader Following_Arrival");
			break;
		case STEERING_MODE_WANDER:
			SceneManager.LoadScene ("Leader Following_Wander");
			break;
		}
	}

	public void OnQueueRepeat () {
		SceneManager.LoadScene ("Queue");
	}
}
