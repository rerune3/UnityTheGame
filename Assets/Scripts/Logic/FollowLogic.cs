using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowLogic : MonoBehaviour {

    private float speed = 0.8f;
	public GameObject targetObject;

    private Animator anim;
	private List<Vector3> path;
    private Vector3 gradualDistanceToMove;
	private Vector3 direction;
	private float actualSpeed;
	private float dist_thresh = 0.08f;

	private Vector3 startingPoint;

//	 Use this for initialization
	void Start () {
		path = new List<Vector3> ();
		anim = GetComponent<Animator> ();

        actualSpeed = speed;
		Waypoint originWaypoint = Utilities.GetClosestWaypointTo (transform.position);
		Waypoint targetWaypoint = Utilities.GetClosestWaypointTo (targetObject.transform.position);
		ConstructPathBetween (originWaypoint, targetWaypoint);

		if (path.Count > 0) {
			Debug.DrawLine (originWaypoint.position, path [0], Color.blue, Mathf.Infinity);
			for (int i = 0; i < path.Count - 1; i++) {
				Debug.DrawLine (path[i], path[i+1], Color.blue, Mathf.Infinity);
			}
		}
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	private void ConstructPathBetween(Waypoint origin, Waypoint target) {
		List<int> visited = new List<int> ();
		ConstructPathHelper (origin, target, visited, 0);
	}

	private void ConstructPathHelper(Waypoint curr, Waypoint target, List<int> visited, int i) {
		if (curr.value == target.value) {
			return;
		}

		i++;
		if (i > 1000) {
			Debug.Log ("Went on too long probably");
			return;
		}

		visited.Add (curr.value);
		Waypoint closestWaypoint = GetWaypointClosestToTarget (curr, target, visited);
		if (closestWaypoint != null) {
			path.Add (closestWaypoint.position);
			ConstructPathHelper (closestWaypoint, target, visited, i);
		}

	}

	private Waypoint GetWaypointClosestToTarget(Waypoint waypoint, Waypoint target, List<int> visited) {
		float shortestDistance = Mathf.Infinity;
		int indexOfClosest = -1;
		float distance = 0.0f;
		// Iterate through adjacent waypoints.
		for (int i = 0; i < waypoint.adjWaypoints.Count; i++) {
			// If adjacent waypoint has not been visited already.
			if (visited.IndexOf(waypoint.adjWaypoints[i].value) == -1) {
				// Check its distance to the target waypoint.
				distance = Vector3.Distance (waypoint.adjWaypoints[i].position, target.position);
				if (distance < shortestDistance) {
					shortestDistance = distance;
					indexOfClosest = i;
				}
			}
		}
		return indexOfClosest == -1 ? null : waypoint.adjWaypoints[indexOfClosest];
	}


    private void MoveNPC()
    {
        gradualDistanceToMove = direction * speed * Time.deltaTime;
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
