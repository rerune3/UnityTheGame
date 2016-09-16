using UnityEngine;
using System.Collections;

public class PlayerEquipLogic : MonoBehaviour {

	private Transform rightArmVessel;
	private Transform leftArmVessel;

	// Use this for initialization
	void Start () {
		rightArmVessel = transform.FindChild ("RightArmVessel");
		leftArmVessel = transform.FindChild ("LeftArmVessel");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.E)) {
			Utilities.instantiate ("Wolf Dagger", rightArmVessel, true);
		}
	}

	private void DestroyChildObjectsOf(GameObject obj) {
		if (obj.transform.childCount > 0) {
			for (int i = 0; i < obj.transform.childCount; i++) {
				Destroy(obj.transform.GetChild (i).gameObject);
			}
		}
	}
}
