using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCMovement : MonoBehaviour
{
	public enum State
	{
		MOVE, REST
	}

	public float speed;
	public Vector3 zoneSize;

	private Animator anim;
	private State currentState;
	private Vector3 gradualDistanceToMove;
	private Vector3 destination;
	private float restDeadlineSec;
	private Vector3 direction;
	private float actualSpeed;
	private Bounds zone;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator>();
        zone = new Bounds(transform.position, zoneSize);

        Utilities.DrawBounds(zone);

        currentState = State.MOVE;
		actualSpeed = speed;

		SetDirectionAndDestination ();
		SetRestDeadline ();
	}
	
	// Update is called once per frame
	void LateUpdate()
	{
        if (currentState == State.MOVE) {
			if (!ReachedDestination ()) {
				MoveNPC ();
			} else {
				currentState = State.REST;
				SetRestDeadline ();
				StopNPC ();
			}
		} else if (currentState == State.REST) {
			if (ReachedRestDeadline ()) {
				SetDirectionAndDestination ();
				currentState = State.MOVE;
			}
		}

	}

	private void MoveNPC()
	{
		actualSpeed = speed;
		gradualDistanceToMove = direction * actualSpeed * Time.deltaTime;
		transform.position += gradualDistanceToMove;
		if (gradualDistanceToMove != Vector3.zero) {
			anim.SetBool ("IsWalking", true);
			anim.SetFloat ("DeltaX", gradualDistanceToMove.x);
			anim.SetFloat ("DeltaY", gradualDistanceToMove.y);
		}
	}

	private void StopNPC() {
		actualSpeed = 0;
		anim.SetBool ("IsWalking", false);
	}

	private bool DestinationHasObstacleInPath() {
		RaycastHit2D raycast = Physics2D.BoxCast (transform.position, 
			new Vector3(0.3f, 0.3f), 0f, direction, Vector3.Distance(transform.position, destination));
        return raycast.collider != null;
	}

	private bool DestinationIsOutsideZone() {
		return !zone.Contains (destination);
	}

	private bool ReachedDestination() {
		return Vector3.Distance (transform.position, destination) < 0.05f;
	}

	private bool ReachedRestDeadline() {
		return Time.time > restDeadlineSec;
	}

	private void SetDirectionAndDestination() {
		float zoneDistance = 0;
		do {
			direction = Utilities.GetRandomDirection ();
			zoneDistance = Vector3.Distance(transform.position, zone.center + Vector3.Scale(zone.extents, direction));
			destination = transform.position + (direction * Random.Range (0, zoneDistance));
		} while (DestinationIsOutsideZone () || DestinationHasObstacleInPath());
	}

	// Change the time the NPC has to rest whenever it comes back to the REST state after the MOVE state.
	private void SetRestDeadline() {
		restDeadlineSec = Time.time + Random.Range (1.0f, 3.5f);
	}

	void OnCollisionEnter2D (Collision2D thingColliding) {
		currentState = State.REST;
		SetRestDeadline ();
		StopNPC ();
	}

}
