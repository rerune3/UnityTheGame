using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waypoint {
	public static Vector3 [] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right};
	public Vector3 position;
	public List<Waypoint> adjWaypoints;
	public bool badWaypoint;
	public int value;

	public Waypoint (Vector3 pos, int v) {
		adjWaypoints = new List<Waypoint>();
		badWaypoint = false;
		position = pos;
		value = v;
	}


}
