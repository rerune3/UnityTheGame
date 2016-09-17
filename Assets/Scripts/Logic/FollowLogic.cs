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

//	 Use this for initialization
	void Start () {
		path = new List<Vector3> ();
		anim = GetComponent<Animator> ();

        actualSpeed = speed;
		ConstructPath (targetObject.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine (transform.position, path[0], Color.blue, Mathf.Infinity);
		Debug.DrawLine (path[0], path[1], Color.blue, Mathf.Infinity);
		Debug.DrawLine (path[1], path[2], Color.blue, Mathf.Infinity);
	}

    private void ConstructPath(Vector3 target)
    {
		Vector2 initalDirection = new Vector3 ((target - transform.position).x, 0).normalized;
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

		float collisionThresh = 0.2f;

		Vector3 displacement = finalDestination - currentPosition;
		RaycastHit2D raycast;


		// Try moving in the +x direction.
		Vector3 tempDestination = currentPosition + new Vector3(displacement.x, 0);
		Vector2 direction = new Vector2 (displacement.x, 0).normalized;
		if (lastDir != direction) {
			raycast = GetObstacleBetween (tempDestination, direction);

			if (raycast.collider == null) {
				// If there is no obstacle in the way.
				path.Add (tempDestination);
				// Recurse.
				ConstructPathHelper (direction, tempDestination, finalDestination, i);
				return;
			} else if (raycast.point.x - transform.position.x > collisionThresh) {
				// If there is an obstacle in the way and it's not too close.
				// Stop just before the obstacle.
				tempDestination = raycast.point + (direction * -1 * collisionThresh);
				path.Add (tempDestination);
				ConstructPathHelper (direction, tempDestination, finalDestination, i);
				return;
			}
		} // end if

		// Try moving in the +y direction.
		tempDestination = currentPosition + new Vector3(0, displacement.y);
		direction = new Vector3 (0, displacement.y).normalized;
		if (lastDir != direction) {
			raycast = GetObstacleBetween (tempDestination, direction);

			if (raycast.collider == null) {
				// If there is no obstacle in the way.
				path.Add (tempDestination);
				// Recurse.
				ConstructPathHelper (direction, tempDestination, finalDestination, i);
				return;
			} else if (raycast.point.y - transform.position.y > collisionThresh) {
				// If there is an obstacle in the way and it's not too close.
				// Stop just before the obstacle.
				tempDestination = raycast.point + (direction * -1 * collisionThresh);
				path.Add (tempDestination);
				ConstructPathHelper (direction, tempDestination, finalDestination, i);
				return;
			}
		}

		// Try moving in the -x direction.
		tempDestination = currentPosition - new Vector3(displacement.x, 0);
		direction = new Vector3 (displacement.x * -1, 0).normalized;
		if (lastDir != direction) {
			raycast = GetObstacleBetween (tempDestination, direction);

			if (raycast.collider == null) {
				// If there is no obstacle in the way.
				path.Add (tempDestination);
				// Recurse.
				ConstructPathHelper (direction, tempDestination, finalDestination, i);
				return;
			} else if (raycast.point.x - transform.position.x > collisionThresh) {
				// If there is an obstacle in the way and it's not too close.
				// Stop just before the obstacle.
				tempDestination = raycast.point + (direction * -1 * collisionThresh);
				path.Add (tempDestination);
				ConstructPathHelper (direction, tempDestination, finalDestination, i);
				return;
			}
		}

		// Try moving in the -y direction.
		tempDestination = currentPosition - new Vector3(0, displacement.y);
		direction = new Vector3 (0, displacement.y * -1).normalized;
		if (lastDir != direction) {
			raycast = GetObstacleBetween (tempDestination, direction);

			if (raycast.collider == null) {
				// If there is no obstacle in the way.
				path.Add (tempDestination);
				// Recurse.
				ConstructPathHelper (direction, tempDestination, finalDestination, i);
				return;
			} else if (raycast.point.y - transform.position.y > collisionThresh) {
				// If there is an obstacle in the way and it's not too close.
				// Stop just before the obstacle.
				tempDestination = raycast.point + (direction * -1 * collisionThresh);
				path.Add (tempDestination);
				ConstructPathHelper (direction, tempDestination, finalDestination, i);
				return;
			}
		}
	}

	private RaycastHit2D GetObstacleBetween(Vector3 destination, Vector3 direction) {
		RaycastHit2D raycast = Physics2D.BoxCast (transform.position, 
			new Vector3(0.3f, 0.3f), 0f, direction, Vector3.Distance(transform.position, destination));
		if (raycast.collider != null) {
			Debug.Log (raycast.collider.name);
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
