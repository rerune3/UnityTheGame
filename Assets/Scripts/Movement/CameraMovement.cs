using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public Transform target;
	public float speed = 0.1f;
	private Camera cam;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
	}

	// Update is called once per frame
	void Update () {
		cam.orthographicSize = (Screen.height / 100f) / 2f;

		if (target) {
			Vector3 defaultCameraZ = new Vector3 (0, 0, -10);
			cam.transform.position = Vector3.Lerp (cam.transform.position, target.transform.position, speed)
				+ defaultCameraZ;
		}
	}
}
