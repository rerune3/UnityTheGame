using UnityEngine;
using System.Collections;

public class NPCIndividualLogic : MonoBehaviour {

	private string speechScript;

	// Use this for initialization
	void Start () {
		speechScript = "Why, hello there. You seem to be new around here. " + 
			"Be careful where you walk around. There are " +
			"dangerous animals roaming around.";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void speakToPlayer() {
		Utilities.displayText (speechScript);
	}
}
