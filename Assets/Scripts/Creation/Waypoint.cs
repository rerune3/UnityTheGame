using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waypoint {
	public enum Dir {UP, DOWN, LEFT, RIGHT, _SIZE}
	public static Vector3 [] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right};
	private Vector3 position;
	private Waypoint[] neighbors;
	private bool badWaypoint;
	private int uid;
	private int numOfNeighbors;

	public Waypoint (Vector3 pos, int id) {
		numOfNeighbors = 0;
		neighbors = new Waypoint[(int)Dir._SIZE]{null, null, null, null};
		badWaypoint = false;
		position = pos;
		uid = id;
	}

	public void AddNeighbor(Waypoint w, int i) {
		if (numOfNeighbors < (int)Dir._SIZE && i < (int)Dir._SIZE && i > -1) {
			numOfNeighbors++;
			neighbors [i] = w;
		}
	}

	public void RemoveNeighbor(int i) {
		if (i < (int)Dir._SIZE && i > -1) {
			numOfNeighbors--;
			neighbors [i] = null;
		}
	}

	public Waypoint GetNeighbor (int i) {
		return neighbors [i];
	}

	public Waypoint[] Neighbors() {
		return neighbors;
	}

	public int NumberOfNeighbors() {
		return numOfNeighbors;
	}

	public int UniqueID() {
		return uid;
	}

	public void SetBadWaypoint(bool bad) {
		badWaypoint = bad;
	}

	public bool IsBadWaypoint() {
		return badWaypoint;
	}

	public Vector3 Position() {
		return position;
	}

}
