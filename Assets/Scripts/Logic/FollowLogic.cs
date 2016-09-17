using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowLogic : MonoBehaviour {

    private float speed = 0.8f;

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
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void ConstructPath(Vector3 target)
    {
        Vector3 displacement = target - transform.position;

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
