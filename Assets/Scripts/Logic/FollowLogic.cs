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

		if (i > 2) {
			Debug.Log ("Probably went on for too long");
			return;
		}

        // Move in the x axis, in the direction of the final destination unless we just moved in the x-axis.
        Vector3 oneComponentDisplacement = new Vector3((finalDestination - currentPosition).x, 0);
        Vector3 start = currentPosition;
        Vector3 location = currentPosition + oneComponentDisplacement;
        Vector2 direction = (location - start).normalized;
        //Utilities.PrintVectors(new []{oneComponentDisplacement, start, location, direction});
        //Debug.Log("Done.");
        if (lastDir != direction && GoToLocation(start, location))
        {
            ConstructPathHelper(direction, start, finalDestination, i);
            return;
        } else
        {
            // Move in the y axis, in the direction of the final destination.
            oneComponentDisplacement = new Vector3(0, (finalDestination - currentPosition).y);
            start = currentPosition;
            location = currentPosition + oneComponentDisplacement;
            direction = (location - start).normalized;
            if (lastDir != direction && GoToLocation(start, location))
            {
                ConstructPathHelper(direction, start, finalDestination, i);
                return;
            }
        }
 
    }

	// Returns whether or not it was able to go to location.
	private bool GoToLocation (Vector3 start, Vector3 location) {
        Vector2 direction = (location - start).normalized;
        float collisionThresh = 0.05f;

        RaycastHit2D raycast = GetObstacleBetween(start, location, direction);
        if (raycast.collider == null)
        {
            path.Add(location);
            return true;
        } else
        {
            float distanceFromObstacle = Vector3.Distance(raycast.point, start);
            if (distanceFromObstacle >= collisionThresh)
            {
                // Move to arbitiary distance just before the obstacle.
                path.Add(raycast.point + (direction * collisionThresh * -1));
                return true;
            } else
            {
                return false;
            }
        }
    }

    private RaycastHit2D GetObstacleBetween(Vector3 start, Vector3 end, Vector3 direction) {
		RaycastHit2D raycast = Physics2D.Linecast (start, end);
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
