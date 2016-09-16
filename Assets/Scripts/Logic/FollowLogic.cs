using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowLogic : MonoBehaviour {

//	public Transform target;
//	private Animator anim;
//	private List<Vector3> destinations;
//	private Vector3 destination;
//	private Vector3 direction;
//	private bool figuringOutNextMove;
//	private float speed = 0.8f;
//	private int runTime = 0;
//	private BoxCollider2D thisCollider;
//	private float obst_dist_thresh = 0.2f;
//	private float dist_thresh = 0.04f;
//	private Vector3 boxColliderPos;
//
//	// Use this for initialization
//	void Start () {
//		anim = GetComponent<Animator> ();
//		thisCollider = GetComponent<BoxCollider2D>();
//		boxColliderPos = transform.TransformPoint(thisCollider.offset);
//
////		Debug.Log ("Displacement " + Utilities.getDisplacement(transform.position, target.position));
//		destinations = new List<Vector3> ();
//		followTarget ();
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		if (!reachedFinalDestination ()) {
//			if (!reachedTemporaryDestination ()) {
//				Vector3 distanceMoved = direction * speed * Time.deltaTime;
//				transform.position += distanceMoved;
//				if (distanceMoved != Vector3.zero) {
//					anim.SetBool ("IsWalking", true);
//					anim.SetFloat ("DeltaX", distanceMoved.x);
//					anim.SetFloat ("DeltaY", distanceMoved.y);
//				} else {
//					anim.SetBool ("IsWalking", false);
//				}
//				distanceMoved = Vector3.zero;
//			} else {
//				setNextDestination ();
//			}
//		}
//	}
//
//	private RaycastHit2D getObstacleInfo (Vector3 curr, Vector3 dir, float dist) {
//		dist = dist < 0 ? dist * -1 : dist;
//		Debug.DrawRay (curr, dir, Color.green, Mathf.Infinity, false);
////		Debug.Log ("Collider size: " + thisCollider.size);
//		float dimension = Mathf.Max (thisCollider.size.x, thisCollider.size.y);
////		Debug.Log ("Dimension: " + dimension);
//		Vector3 size = new Vector3 (dimension, dimension);
//		return Physics2D.BoxCast (curr, size, 0.0f, dir);
////		return Physics2D.CircleCast (curr, 0.05f, dir, dist);
//	}
//
//	private Vector3 getDeltaNeededToEvadeObstacle(Collider2D col, float currDelta, char axis) {
//		int currDeltaSign = currDelta < 0 ? -1 : 1;
////		float arbitiaryDelta = Mathf.Abs(Mathf.Max (thisCollider.size.x, thisCollider.size.y));//0.8f;
//		float arbitiaryDelta = 0.8f;
//		if (axis == 'x') {
//			if (Mathf.Abs (currDelta) > arbitiaryDelta)
//				return new Vector3 (currDelta, 0);
//			else 
//				return new Vector3 (arbitiaryDelta*currDeltaSign, 0);
//		} else {
//			if (Mathf.Abs (currDelta) > arbitiaryDelta)
//				return new Vector3 (0, currDelta);
//			else 
//				return new Vector3 (0, arbitiaryDelta*currDeltaSign);
//		}
//	}
//
//	private Vector3 getDeltaNeededToNotHitObstacle (float obstDist, float currDelta, char axis){
//		int sign = currDelta < 0 ? -1 : 1;
//		if (axis == 'x') {
//			if (Mathf.Abs (currDelta) < obstDist) {
//				return new Vector3 (currDelta, 0);
//			} else if (obstDist > 0.2f) {
//				return new Vector3 ((obstDist - obst_dist_thresh)*sign, 0);
//			} else {
//				return Vector3.zero;
//			}
//		} else {
//			if (Mathf.Abs (currDelta) < obstDist) {
//				return new Vector3 (0, currDelta);
//			} else if (obstDist > 0.2f) {
//				return new Vector3 (0, (obstDist - obst_dist_thresh)*sign);
//			} else {
//				return Vector3.zero;
//			}
//		}
//	}
//
//	private void pathFinder(Vector3 curr, char currAxis){
//		if (!(Vector3.Distance (curr, target.position) < dist_thresh)) {
//			Vector3 delta = Utilities.getDisplacement (curr, target.position);
//			Vector3 x_dir = new Vector3 (delta.x, 0).normalized;
//			Vector3 y_dir = new Vector3 (0, delta.y).normalized;
//			RaycastHit2D x_obst_hit = getObstacleInfo (curr, x_dir, delta.x);
//			RaycastHit2D y_obst_hit = getObstacleInfo (curr, y_dir, delta.y);
//			float x_obst_dist = x_obst_hit.distance;
//			float y_obst_dist = y_obst_hit.distance;
//			Vector3 newCurr = Vector3.zero;
//			char newCurrAxis = ' ';
//			Vector3 obstDelta = Vector3.zero;
//
//			if (x_obst_dist == 0 && y_obst_dist == 0) {
//				Debug.Log ("h");
//				if (currAxis == 'y') {
//					obstDelta = new Vector3 (delta.x, 0);
//					newCurrAxis = 'x';
//				} else {
//					obstDelta = new Vector3 (0, delta.y);
//					newCurrAxis = 'y';
//				}
//			} else if (x_obst_dist == 0 && y_obst_dist > 0) {
//				Debug.Log ("hh");
//				if (currAxis == 'y') {
//					newCurrAxis = 'x';
//					obstDelta = getDeltaNeededToEvadeObstacle (y_obst_hit.collider, 
//						delta.x, newCurrAxis);
//				} else {
//					newCurrAxis = 'y';
//					obstDelta = getDeltaNeededToNotHitObstacle(y_obst_dist, delta.y, newCurrAxis);
//				}
//			} else if (y_obst_dist == 0 && x_obst_dist > 0) {
//				Debug.Log ("hhh");
//				if (currAxis == 'x') {
//					newCurrAxis = 'y';
//					obstDelta = getDeltaNeededToEvadeObstacle (x_obst_hit.collider, 
//						delta.y, newCurrAxis);
//				} else {
//					newCurrAxis = 'x';
//					obstDelta = getDeltaNeededToNotHitObstacle(x_obst_dist, delta.x, newCurrAxis);
//				}
//			} else if (x_obst_dist > y_obst_dist) {
//				Debug.Log ("hhhhh");
//				if (currAxis == 'y') {
//					newCurrAxis = 'x';
//					int sign = delta.x < 0 ? -1 : 1;
//					obstDelta = getDeltaNeededToEvadeObstacle (y_obst_hit.collider, 
//						(x_obst_dist - obst_dist_thresh) * sign, newCurrAxis);
//				} else {
//					newCurrAxis = 'y';
//					obstDelta = getDeltaNeededToNotHitObstacle(y_obst_dist, delta.y, newCurrAxis);
//				}
//			} else if (y_obst_dist > x_obst_dist) {
//				Debug.Log ("hhhhhh");
//				if (currAxis == 'x') {
//					newCurrAxis = 'y';
//					int sign = delta.y < 0 ? -1 : 1;
//					obstDelta = getDeltaNeededToEvadeObstacle (x_obst_hit.collider, 
//						(y_obst_dist - obst_dist_thresh) * sign, newCurrAxis);
//				} else {
//					newCurrAxis = 'x';
//					obstDelta = getDeltaNeededToNotHitObstacle(x_obst_dist, delta.x, newCurrAxis);
//				}
//			}
//			newCurr = curr + obstDelta;
////			Debug.Log ("Displacement " + delta);
////			Debug.Log ("x_dir: " + x_dir);
////			Debug.Log ("y_dir: " + y_dir);
////			Debug.Log ("x_obst_dist: " + x_obst_dist);
////			Debug.Log ("y_obst_dist: " + y_obst_dist);
////			Debug.Log ("Adding: " + obstDelta);
////			Debug.Log ("prevAxis: " + currAxis);
////			Debug.Log ("newCurr: " + newCurr);
//			destinations.Add (newCurr);
//			runTime++;
//			if (runTime < 50)
//				pathFinder (newCurr, newCurrAxis);
//
////			if (x_obst_dist == 0 && y_obst_dist == 0) {
////				if (currAxis == 'y') {
////					obstDelta = new Vector3 (delta.x, 0);
////					newCurrAxis = 'x';
////				} else {
////					obstDelta = new Vector3 (0, delta.y);
////					newCurrAxis = 'y';
////				}
////			} else if (x_obst_dist == 0 && y_obst_dist > 0) {
////				newCurrAxis = 'x';
////				obstDelta = getDeltaNeededToEvadeObstacle(y_obst_hit.collider, 
////					delta.x, newCurrAxis);
////				Debug.Log ("hello2");
////			} else if (y_obst_dist == 0 && x_obst_dist > 0) {
////				newCurrAxis = 'y';
////				obstDelta = getDeltaNeededToEvadeObstacle(x_obst_hit.collider, 
////					delta.y, newCurrAxis);
////			} else if (x_obst_dist > y_obst_dist) {
////				newCurrAxis = 'x';
////				int sign = delta.x < 0 ? -1 : 1;
////				obstDelta = getDeltaNeededToEvadeObstacle(y_obst_hit.collider, 
////					(x_obst_dist - 0.2f)*sign, newCurrAxis);
////			} else if (y_obst_dist > x_obst_dist) {
////				newCurrAxis = 'y';
////				int sign = delta.y < 0 ? -1 : 1;
////				obstDelta = getDeltaNeededToEvadeObstacle(x_obst_hit.collider, 
////					(y_obst_dist - 0.2f)*sign, newCurrAxis);
////			}
////			newCurr = curr + obstDelta;
////			Debug.Log ("Displacement " + delta);
////			Debug.Log ("x_dir: " + x_dir);
////			Debug.Log ("y_dir: " + y_dir);
////			Debug.Log ("x_obst_dist: " + x_obst_dist);
////			Debug.Log ("y_obst_dist: " + y_obst_dist);
////			Debug.Log ("Adding: " + obstDelta);
////			Debug.Log ("newCurr: " + newCurr + "\n\n");
////			destinations.Add (newCurr);
////			runTime++;
////			if (runTime < 10)
////				pathFinder (newCurr, newCurrAxis);
//		}
//	}
//
//	private void findPath() {
//		Vector3 delta = Utilities.getDisplacement (transform.position, target.position);
//		Vector3 pos = transform.TransformPoint(thisCollider.offset);
//		pathFinder (pos, 'x');
//		for (int i = 0; i < destinations.Count; i++)
//			Debug.Log (i + " : " + destinations[i]);
//	}
//
//	private void followTarget() {
//		findPath ();
//		Vector3 pos = transform.TransformPoint(thisCollider.offset);
//		if (destinations.Count > 0) {
//			direction = Utilities.getDisplacement(pos, destinations [0]).normalized;
//			direction = Utilities.furtherNormalizeDirection (direction);
//		}
//	}
//
//	private void setNextDestination () {
//		Vector3 pos = transform.TransformPoint(thisCollider.offset);
//		destinations.RemoveAt (0);
//		if (destinations.Count > 0) {
//			direction = Utilities.getDisplacement(pos, destinations [0]).normalized;
//			direction = Utilities.furtherNormalizeDirection (direction);
//		}
//	}
//
//	private bool reachedTemporaryDestination () {
//		Vector3 pos = transform.TransformPoint(thisCollider.offset);
//		if (destinations.Count > 0) {
////			Debug.Log ("Distance: " + Vector3.Distance (pos, destinations [0]));
//			return Vector3.Distance (pos, destinations [0]) < dist_thresh;
//		}
//		return true;
//	}
//
//	private bool reachedFinalDestination () {
//		Vector3 pos = transform.TransformPoint(thisCollider.offset);
//		return Vector3.Distance (pos, target.position) < dist_thresh;
//	}
//
//	public void setTargetToFollow(Transform t) {
//		target = t;
//	}
//
//	void OnCollisionEnter2D (Collision2D thingColliding) {
////		setNextDestination ();
//	}

	private Animator anim;
	private GameObject scapegoat;
	private GameObject waypointObj;
	private List<Vector3> path;
	private Vector3 direction;
	private float speed = 0.8f;
	private float dist_thresh = 0.08f;

//	 Use this for initialization
	void Start () {
		path = new List<Vector3> ();
		anim = GetComponent<Animator> ();
		scapegoat = gameObject;
//		findPathToWaypoint ();
	}
	
	// Update is called once per frame
	void Update () {

		if (!reachedTemporaryWaypoint ()) {
			Vector3 distanceMoved = direction * speed * Time.deltaTime;
			transform.position += distanceMoved;
			if (distanceMoved != Vector3.zero) {
				anim.SetBool ("IsWalking", true);
				anim.SetFloat ("DeltaX", distanceMoved.x);
				anim.SetFloat ("DeltaY", distanceMoved.y);
			} else {
				anim.SetBool ("IsWalking", false);
			}
			distanceMoved = Vector3.zero;
		} else {
			setNextTemporaryWaypoint ();
		}

	}

	private void setNextTemporaryWaypoint () {
		if (path.Count > 0) 
			path.RemoveAt (0);
		if (path.Count > 0)
			direction = path [0].normalized;
	}

	private bool reachedTemporaryWaypoint () {
		if (path.Count > 0) {
			direction = path [0].normalized;
			Debug.Log ("wow: " +  Vector3.Distance (transform.position, transform.position + path[0]));
			return Vector3.Distance (transform.position, transform.position + path[0]) < dist_thresh;
		}
		direction = Vector3.zero;
		return true;
	}

	private void findPathToWaypoint() {
		if (waypointObj != null) {
			Vector3 delta = Utilities.getDisplacement (transform.position, waypointObj.transform.position);
			path.Add (new Vector3(delta.x, 0));
			path.Add (new Vector3(0, delta.y));
		} else {
			Debug.LogError ("Uhh..There's no waypoint. Awk.");
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.name == "Waypoint") {
			if (!other.gameObject.GetComponent<WaypointLogic> ().waypoint.badWaypoint) {
				if (waypointObj == null) {
					Debug.Log ("should only happen once.");
					waypointObj = other.gameObject;
					Debug.Log ("NPC: " + transform.position);
					Debug.Log ("destination: " + waypointObj.transform.position);
					findPathToWaypoint ();
				} else {
					waypointObj = other.gameObject;
				}

			}
		}
	}
	
}
