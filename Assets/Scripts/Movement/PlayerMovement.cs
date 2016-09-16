using UnityEngine;
using System.Collections;

public class PlayerMovement : Entity {

	private Rigidbody2D rBody;
	private Animator anim;

	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		speed = 1.0f;
	}

	// Update is called once per frame
	void Update () {
		Vector3 distanceToMove = new Vector3 ();

		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			distanceToMove += Vector3.up * this.speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			distanceToMove += Vector3.left * this.speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			distanceToMove += Vector3.down * this.speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			distanceToMove += Vector3.right * this.speed * Time.deltaTime;
		} 

		if (Input.GetKeyUp(KeyCode.Space)) {
			anim.SetTrigger("IsAttacking");
		}

		rBody.transform.position += distanceToMove;
			
		if (distanceToMove != Vector3.zero) {
			anim.SetBool ("IsWalking", true);
			anim.SetFloat ("DeltaX", distanceToMove.x);
			anim.SetFloat ("DeltaY", distanceToMove.y);
		} else {
			anim.SetBool ("IsWalking", false);
		}

	}
}
