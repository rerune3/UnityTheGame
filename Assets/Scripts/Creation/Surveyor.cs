using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Surveyor : MonoBehaviour {

	public Vector2 mapSize;
	public float exportScale;
	public float spaceBetweenPoints;
	private Waypoint [][] waypoints;
	private Vector3 numWaypoints;

	void Awake() {
		numWaypoints = mapSize / spaceBetweenPoints;
		mapSize = mapSize * exportScale;
		spaceBetweenPoints = spaceBetweenPoints * exportScale;

		createWaypointArray ();
		createWaypoints();
		connectWaypoints();
		identifyBadWaypoints ();
		drawWaypointConnections ();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
		
	public Waypoint [][] GetWaypoints() {
		return waypoints;
	}

	private void identifyBadWaypoints () {
		Waypoint w;
		for (int i = 0; i < waypoints.Length; i++) {
			for (int j = 0; j < waypoints [i].Length; j++) {
				w = waypoints [i] [j];
				if (w.adjWaypoints.Count < 3) {
					w.badWaypoint = true;
				}
			}
		}
	}

	private void drawWaypointConnections() {
		Waypoint w;
		for (int i = 0; i < waypoints.Length; i++) {
			for (int j = 0; j < waypoints [i].Length; j++) {
				w = waypoints [i] [j];
				for (int k = 0; k < w.adjWaypoints.Count; k++) {
					Waypoint adjWaypoint = w.adjWaypoints [k];
					Debug.DrawLine (w.position, adjWaypoint.position, Color.green, Mathf.Infinity);
				}
			}
		}
	}

	private void connectWaypoints () {
		Waypoint waypoint;
		Waypoint otherWaypoint;
		Vector3 dir;
		for (int i = 0; i < waypoints.Length; i++) {
			for (int j = 0; j < waypoints[i].Length; j++) {
				waypoint = waypoints [i] [j];
				for (int k = 0; k < Waypoint.directions.Length; k++) {
					dir = Waypoint.directions [k];
					if (i - dir.y > -1 && i - dir.y < waypoints.Length &&
						j + dir.x > -1 && j + dir.x < waypoints [i].Length) {
						otherWaypoint = waypoints[(int)(i - dir.y)][(int)(j + dir.x)];
						RaycastHit2D hit = Physics2D.Linecast(waypoint.position, otherWaypoint.position);
						if (hit.collider == null || hit.collider.name != "Collision") {
							waypoints [i] [j].adjWaypoints.Add (otherWaypoint);
						}
					}
				}
			}
		}
	}

	private void createWaypoints() {
		Vector3 origin = transform.position;
		for (int i = 0; i < waypoints.Length; i++) {
			for (int j = 0; j < waypoints[i].Length; j++) {
				Vector3 position = new Vector3 (origin.x + spaceBetweenPoints * j, 
					origin.y - spaceBetweenPoints * i);
				waypoints [i] [j] = new Waypoint (position, (i * waypoints[i].Length) + j);
			}
		}
	}

	private void removeWaypoint(List<Vector2> waypoints, Vector2 itemToDelete) {
		for (int i = 0; i < waypoints.Count; i++) {
			if (waypoints [i] == itemToDelete) {
				waypoints.RemoveAt (i);
				break;
			}
		}
	}

	private void createWaypointArray() {
		waypoints = new Waypoint[(int)numWaypoints.y + 1][];
		for (int i = 0; i < waypoints.Length; i++)
			waypoints[i] = new Waypoint[(int)numWaypoints.x + 1];
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.name == "Collision") {
//			Debug.Log (other.gameObject.name + " BANG");
		}
	}
}
