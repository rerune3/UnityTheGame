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
		startingPoint = transform.position;
		ConstructPath (targetObject.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
		if (path.Count > 0) {
			Debug.DrawLine (startingPoint, path [0], Color.blue, Mathf.Infinity);
			for (int i = 0; i < path.Count - 1; i++) {
				Debug.DrawLine (path[i], path[i+1], Color.blue, Mathf.Infinity);
			}
		}
	}

    private void ConstructPath(Vector3 target)
    {
		Vector2 initalDirection = new Vector3 ((target - startingPoint).x, 0).normalized;
		ConstructPathHelper (initalDirection*-1, transform.position, target, 0);

		Debug.Log ("Path: \n");
		for (int i = 0; i < path.Count; i++) {
			Debug.Log (path[i]);
		}
    }

	private void ConstructPathHelper(Vector2 lastDir, Vector3 currentPosition, Vector3 finalDestination, int i) {
		// If already at destination.
		if (Vector3.Distance (currentPosition, finalDestination) < 0.05f) {
			return;
		}

		i++;

		if (i > 12) {
			Debug.Log ("Probably went on for too long");
			return;
		}

		float collisionThresh = 0.05f;
		float arbitiaryMovementDistance = 1.5f;

		Vector3 displacement = finalDestination - currentPosition;
		RaycastHit2D raycast;
		Vector3 tempDestination;
		Vector2 direction;

		// If havent reached x destination.
		if (currentPosition.x != finalDestination.x) {
			if (true) {
				// Try moving in the x direction, in the direction of the target.
				tempDestination = currentPosition + new Vector3 (displacement.x, 0);
				direction = new Vector2 (displacement.x, 0).normalized;
				if (lastDir != direction && direction != Vector2.zero && lastDir.x == 0) {
					raycast = GetObstacleBetween (currentPosition, tempDestination, direction);
			
					if (raycast.collider == null) {
						// If there is no obstacle in the way.
						path.Add (tempDestination);
						// Recurse.
						ConstructPathHelper (direction, tempDestination, finalDestination, i);
						return;
					} else if (Mathf.Abs (raycast.point.x - currentPosition.x) > collisionThresh) {
						// If there is an obstacle in the way and it's not too close.
						// Stop just before the obstacle.
						tempDestination = raycast.point + (direction * -1 * collisionThresh);
						path.Add (tempDestination);
						ConstructPathHelper (direction, tempDestination, finalDestination, i);
						return;
					}
				} // end if
			}
		}

		// If havent reached y destination.
		if (currentPosition.y != finalDestination.y) {
			// Try moving in the y direction, in the direction of the target.
			tempDestination = currentPosition + new Vector3 (0, displacement.y);
			direction = new Vector3 (0, displacement.y).normalized;
			if (lastDir != direction && direction != Vector2.zero && lastDir.y == 0) {
				raycast = GetObstacleBetween (currentPosition, tempDestination, direction);

				if (raycast.collider == null) {
					// If there is no obstacle in the way.
					path.Add (tempDestination);
					// Recurse.
					ConstructPathHelper (direction, tempDestination, finalDestination, i);
					return;
				} else if (Mathf.Abs (raycast.point.y - currentPosition.y) > collisionThresh) {
					// If there is an obstacle in the way and it's not too close.
					// Stop just before the obstacle.
					tempDestination = raycast.point + (direction * -1 * collisionThresh);
					path.Add (tempDestination);
					ConstructPathHelper (direction, tempDestination, finalDestination, i);
					return;
				}
			}
		}

		float xSign = displacement.x / Mathf.Abs (displacement.x);
		float ySign = displacement.y / Mathf.Abs (displacement.y);

		// Is arbitiary.
		Vector3 newDisplacement = new Vector3 (arbitiaryMovementDistance * xSign, arbitiaryMovementDistance * ySign);

		// Try moving in the x direction, the opposite direction of the target.
		tempDestination = currentPosition + new Vector3(newDisplacement.x, 0);
		direction = new Vector3 (newDisplacement.x, 0).normalized;
		if (lastDir != direction && direction != Vector2.zero && lastDir.x == 0) {
			raycast = GetObstacleBetween (currentPosition, tempDestination, direction);

			if (raycast.collider == null) {
				// If there is no obstacle in the way.
				path.Add (tempDestination);
				// Recurse.
				ConstructPathHelper (direction, tempDestination, finalDestination, i);
				return;
			} else if (Mathf.Abs(raycast.point.x - currentPosition.x) > collisionThresh) {
				// If there is an obstacle in the way and it's not too close.
				// Stop just before the obstacle.
				tempDestination = raycast.point + (direction * -1 * collisionThresh);
				path.Add (tempDestination);
				ConstructPathHelper (direction, tempDestination, finalDestination, i);
				return;
			}
		}

		// Try moving in the y direction, the opposite direction of the target.
		tempDestination = currentPosition + new Vector3(0, newDisplacement.y);
		direction = new Vector3 (0, newDisplacement.y).normalized;
		if (lastDir != direction && direction != Vector2.zero && lastDir.y == 0) {
			raycast = GetObstacleBetween (currentPosition, tempDestination, direction);

			if (raycast.collider == null) {
				// If there is no obstacle in the way.
				path.Add (tempDestination);
				// Recurse.
				ConstructPathHelper (direction, tempDestination, finalDestination, i);
				return;
			} else if (Mathf.Abs(raycast.point.y - currentPosition.y) > collisionThresh) {
				// If there is an obstacle in the way and it's not too close.
				// Stop just before the obstacle.
				tempDestination = raycast.point + (direction * -1 * collisionThresh);
				path.Add (tempDestination);
				ConstructPathHelper (direction, tempDestination, finalDestination, i);
				return;
			}
		}
	}

	// Keep working on this.
//	private bool goToLocation (Vector3 currPos, Vector3 destination) {
//	}

	private RaycastHit2D GetObstacleBetween(Vector3 start, Vector3 end, Vector3 direction) {
		RaycastHit2D raycast = Physics2D.Linecast (start, end);
		if (raycast.collider != null) {
			if (raycast.collider.name == "Player") {
				return new RaycastHit2D();
			}
		}
		return raycast;
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
