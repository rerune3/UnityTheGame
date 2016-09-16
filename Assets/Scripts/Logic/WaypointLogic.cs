using UnityEngine;
using System.Collections;

public class WaypointLogic : MonoBehaviour {

	public bool safe = true;
	public Waypoint waypoint;

	void Awake() {
	}

	public void setWaypoint (Waypoint w) {
		waypoint = w;
		waypoint.badWaypoint = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "Collision") {
			waypoint.badWaypoint = !safe;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.name == "Collision") {
			waypoint.badWaypoint = !safe;
		}
	}

}
