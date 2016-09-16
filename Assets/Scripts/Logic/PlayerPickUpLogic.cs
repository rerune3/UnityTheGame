using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPickUpLogic : MonoBehaviour {

	private Inventory playerInventory;

	private string key;
	private GameObject objectPickingUp;

	void Start () {
		playerInventory = new Inventory ();
	}

	void Update () {
		if (Input.GetKeyUp (KeyCode.B) && objectPickingUp != null) {
			pickUpItem (key);
			playerInventory.printInventory ();
		} else if (Input.GetKeyUp (KeyCode.N) && key != null) {
			dropItem (key);
			playerInventory.printInventory ();
		}
	}

	void OnTriggerEnter2D(Collider2D thingCollidedWith) {
		if (thingCollidedWith != null && thingCollidedWith.tag.Equals("Item")) {
			key = thingCollidedWith.name;
			objectPickingUp = thingCollidedWith.gameObject;
		}
	}

	private void pickUpItem (string key) {
		playerInventory.addItemToInventory (ItemBankWrapper.getItemData (key));
		Destroy (objectPickingUp);
	}

	private void dropItem (string key) {
		ItemData itemData = ItemBankWrapper.getItemData (key);
		GameObject obj = (GameObject)
			Instantiate (ItemBankWrapper.getItemObject(key),  transform.position, transform.rotation);
		obj.name = itemData.ItemName;
		playerInventory.removeItemFromInventory (itemData);
	}
}
