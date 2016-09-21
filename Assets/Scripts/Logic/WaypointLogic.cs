using UnityEngine;
using System.Collections;

public class WaypointLogic : MonoBehaviour {

	public Waypoint waypoint;

	void Awake() {
	}

	public void setWaypoint (Waypoint w) {
		waypoint = w;
		waypoint.SetBadWaypoint(false);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "Collision") {
			waypoint.SetBadWaypoint(true);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.name == "Collision") {
			waypoint.SetBadWaypoint(true);
		}
	}

}
