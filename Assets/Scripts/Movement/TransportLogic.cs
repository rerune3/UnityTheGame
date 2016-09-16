using UnityEngine;
using System.Collections;

public class TransportLogic : MonoBehaviour {

	public Transform transportDestination;

	void Start() {
	}

	void OnTriggerEnter2D(Collider2D thingColliding) {
		if (thingColliding != null) {
//			GUI.Label(new Rect(10, 10, 100, 20), "Hello World!");
//			thingColliding.transform.position = transportDestination.position;
		}
	}
}
