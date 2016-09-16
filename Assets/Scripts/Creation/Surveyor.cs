using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Surveyor : MonoBehaviour {

	public Vector2 mapSize;
	public float exportScale;
	public float spaceBetweenPoints;
	private Waypoint [][] waypoints;
	private Vector3 numWaypoints;
	private Vector3 originalPosition;

	void Awake() {
		numWaypoints = mapSize / spaceBetweenPoints;
		mapSize = mapSize * exportScale;
		spaceBetweenPoints = spaceBetweenPoints * exportScale;
		createWaypointArray ();

		originalPosition = new Vector3 (0, 0);
		transform.localPosition = originalPosition;

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

	private void identifyBadWaypoints () {
		Waypoint w;
		Waypoint otherW;
		for (int i = 0; i < waypoints.Length; i++) {
			for (int j = 0; j < waypoints [i].Length; j++) {
				w = waypoints [i] [j];
				if (w.indexAdjWaypoints.Count < 3) {
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
				for (int k = 0; k < w.indexAdjWaypoints.Count; k++) {
					Vector2 index = w.indexAdjWaypoints [k];
					Debug.DrawLine (w.obj.transform.position, 
						waypoints[(int)index.x][(int)index.y].obj.transform.position,  Color.green, 
						Mathf.Infinity);
				}
			}
		}
	}

	private void connectWaypoints () {
		GameObject obj;
		Vector3 dir;
		for (int i = 0; i < waypoints.Length; i++) {
			for (int j = 0; j < waypoints[i].Length; j++) {
				obj = waypoints [i] [j].obj;
				for (int k = 0; k < Waypoint.directions.Length; k++) {
					dir = Waypoint.directions [k];
					RaycastHit2D hit = Physics2D.Raycast (obj.transform.position, 
						dir, spaceBetweenPoints + 0.3f);
					if (hit.collider == null || hit.collider.name != "Collision") { // TODO: Refine this condition
						if (i - dir.y > -1 && i - dir.y < waypoints.Length &&
							j + dir.x > -1 && j + dir.x < waypoints [i].Length && 
							!waypoints[(int)(i - dir.y)][(int)(j + dir.x)].badWaypoint) {
							waypoints [i] [j].indexAdjWaypoints.Add (new Vector2(i - dir.y, j + dir.x));
						}
					}
				}
			}
		}
	}

	private void createWaypoints() {
		for (int i = 0; i < waypoints.Length; i++) {
			for (int j = 0; j < waypoints[i].Length; j++) {
				GameObject obj = Utilities.instantiateMinorPrefab ("Waypoint", transform, 
					new Vector3 (spaceBetweenPoints*j, spaceBetweenPoints*i*-1), true);
				obj.name = "Waypoint";
				waypoints [i] [j] = new Waypoint (obj, waypoints.Length);
				obj.GetComponent<WaypointLogic> ().setWaypoint(waypoints [i] [j]);
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
