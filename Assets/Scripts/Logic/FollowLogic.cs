using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowLogic : MonoBehaviour {

    private float speed = 3;
	public GameObject targetObject;
	private Vector3 targetPosition;

    private Animator anim;
	private List<Vector3> path;
	private List<Vector3> newPath;
    private Vector3 gradualDistanceToMove;
	private Vector3 direction;
	private float actualSpeed;
	private float dist_thresh = 0.08f;

	private Vector3 startingPoint;

//	 Use this for initialization
	void Start () {
		path = new List<Vector3> ();
		newPath = new List<Vector3> ();

		anim = GetComponent<Animator> ();
		targetPosition = targetObject.transform.position;

        actualSpeed = speed;
//		GetNewPathToTargetObject ();

//		Waypoint originWaypoint = Utilities.GetClosestWaypointTo (targetObject.transform.position);
//		Debug.Log (originWaypoint.Position ());
//		Debug.Log (targetObject.transform.position);

    }
	
	// Update is called once per frame
	void Update () {
//		Waypoint originWaypoint = Utilities.GetClosestWaypointTo (targetObject.transform.position);
//
		if (targetPosition != targetObject.transform.position) {
			GetNewPathToTargetObject();
		}

		Move();

	}

	private void UpdatePathWithNewPath() {
//		path.AddRange (newPath);
//		path = newPath;
	}

	private void Move() {
		if (path.Count > 1) {
			if (Vector3.Distance(transform.position, path [0]) < 0.04f) {
				path.RemoveAt (0);
				direction = path [0] - transform.position;
			}
			MoveNPC ();
		} else {
			StopNPC ();
		}

	}

	private void GetNewPathToTargetObject() {
		path.Clear ();

		Vector3 startingPosition;
		if (path.Count > 0) {
			startingPosition = path [path.Count - 1];
		} else {
			startingPosition = transform.position;
		}

		// Waypoint CLOSEST to the current position of object attached to this script.
		Waypoint originWaypoint = Utilities.GetClosestWaypointTo (startingPosition);
		// Waypoint CLOSEST to the target.
		Waypoint targetWaypoint = Utilities.GetClosestWaypointTo (targetObject.transform.position);

		ConstructPathBetween (originWaypoint, targetWaypoint);
		AttachPathToCurrentAndTargetObject (startingPosition, originWaypoint, targetWaypoint);

		if (path.Count > 0) {
			for (int i = 0; i < path.Count - 1; i++) {
				Debug.DrawLine (path[i], path[i+1], Color.blue, 5);
			}
		}

		targetPosition = targetObject.transform.position;
		UpdatePathWithNewPath ();

	}

	private void AttachPathToCurrentAndTargetObject(Vector3 startingPosition, Waypoint origin, Waypoint target) {
		// Appends a path from current object to current path and then current path to target object.
		Vector3 displacementFromCurr = origin.Position () - startingPosition;
		Vector3 displacementFromTarget = target.Position() - targetObject.transform.position;

		path.Insert (0, transform.position + new Vector3(0, displacementFromCurr.y));
		path.Insert (0, transform.position);

//		path.Add (targetObject.transform.position + new Vector3(displacementFromTarget.x, 0));
//		path.Add (targetObject.transform.position);
	}

	private void ConstructPathBetween(Waypoint origin, Waypoint target) {
		List<int> visited = new List<int> ();

		path.Add (origin.Position());
		ConstructPathHelper (origin, target, visited, 0);
		path.Add (target.Position());
	}

	private void ConstructPathHelper(Waypoint curr, Waypoint target, List<int> visited, int i) {
		if (curr.UniqueID() == target.UniqueID()) {
			return;
		}

		i++;
		if (i > 1000) {
			Debug.Log ("Went on too long probably");
			return;
		}

		visited.Add (curr.UniqueID());
		Waypoint closestWaypoint = GetWaypointClosestToTarget (curr, target, visited);
		if (closestWaypoint != null) {
			path.Add (closestWaypoint.Position());
			ConstructPathHelper (closestWaypoint, target, visited, i);
		}

	}

	private Waypoint GetWaypointClosestToTarget(Waypoint waypoint, Waypoint target, List<int> visited) {
		float shortestDistance = Mathf.Infinity;
		int indexOfClosest = -1;
		float distance = 0.0f;
		// Iterate through adjacent waypoints.
		for (int i = 0; i < waypoint.Neighbors().Length; i++) {
			// If adjacent waypoint has not been visited already.
			if (waypoint.GetNeighbor(i) != null && !waypoint.GetNeighbor(i).IsBadWaypoint() 
				&& visited.IndexOf(waypoint.GetNeighbor(i).UniqueID()) == -1) {
				// Check its distance to the target waypoint.
				distance = Vector3.Distance (waypoint.GetNeighbor(i).Position(), target.Position());
				if (distance < shortestDistance) {
					shortestDistance = distance;
					indexOfClosest = i;
				}
			}
		}
		return indexOfClosest == -1 ? null : waypoint.GetNeighbor(indexOfClosest);
	}


    private void MoveNPC()
    {
		actualSpeed = speed;
		gradualDistanceToMove = direction * actualSpeed * Time.deltaTime;
        transform.position += gradualDistanceToMove;
        if (gradualDistanceToMove != Vector3.zero)
        {
            anim.SetBool("IsWalking", true);
            anim.SetFloat("DeltaX", gradualDistanceToMove.x);
            anim.SetFloat("DeltaY", gradualDistanceToMove.y);
        }
    }

    private void StopNPC()
    {
        actualSpeed = 0;
        anim.SetBool("IsWalking", false);
    }

}
