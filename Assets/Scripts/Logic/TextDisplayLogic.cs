using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextDisplayLogic : MonoBehaviour{

	private GameObject textPanel;
	private Text textObject;
	private float originalSpeed;
	private float displaySpeed;
	private bool animating;
	public static TextDisplayLogic instance;

	void Start() {
		textPanel = this.gameObject;
		textObject = textPanel.GetComponentInChildren<Text>();
		textPanel.SetActive (false); 

//		displaySpeed = 0.05f;
		displaySpeed = 0.0f;
		originalSpeed = displaySpeed;
	}

	void Update() {
		if (textDisplayIsShowing () && Input.GetKeyUp (KeyCode.Return)) {
			if (animating) {
				displaySpeed = 0;
			} else {
				hideTextDisplayPanel ();
			}
		}
	}

	public bool textDisplayIsShowing() {
		return textPanel.activeSelf;
	}

	public void showTextDisplayPanel () {
		textPanel.SetActive (true); 
	}

	public void hideTextDisplayPanel () {
		textPanel.SetActive (false); 
		textObject.text = "";
	}

	public void displayText (string str) {
		displaySpeed = originalSpeed;
		StartCoroutine ("animateText", str);
	}

	IEnumerator animateText(string text) {
		animating = true;
		for (int i = 0; i < text.Length; i++) {
			textObject.text += text[i];
			yield return new WaitForSeconds (displaySpeed); 
		}
		animating = false;
	}
}
