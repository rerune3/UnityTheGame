using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waypoint {
	public enum Dir {UP, DOWN, LEFT, RIGHT, _SIZE};
	public static Vector3 [] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right};
	public GameObject obj;
	public List<Vector2> indexAdjWaypoints;
	public int index;
	public bool badWaypoint;

	public Waypoint (GameObject o, int i) {
		indexAdjWaypoints = new List<Vector2>();
		obj = o;
		index = i;
	}


}
