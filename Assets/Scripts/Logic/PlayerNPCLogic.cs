using UnityEngine;
using System.Collections;

public class PlayerNPCLogic : MonoBehaviour {

	private GameObject NPC;

	void Start () {
		NPC = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (NPC != null) {
			if (Input.GetKeyUp (KeyCode.LeftShift) && NPC.tag == "Individual") {
				NPC.SendMessage ("follow", transform);
//				NPC.SendMessage ("speakToPlayer");
			}
		}
	}

	void OnCollisionEnter2D (Collision2D other) {
		NPC = other.gameObject;
	} 

	void OnCollisionExit2D (Collision2D other) {
		NPC.SendMessage ("resumeNPCMovement");
		NPC = null;
		Utilities.hideTextDisplay ();
	} 
}
