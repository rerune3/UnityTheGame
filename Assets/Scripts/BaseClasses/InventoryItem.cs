using UnityEngine;
using System.Collections;

public class InventoryItem {

	private ItemData item;
	private int itemAmount;

	public InventoryItem (ItemData item, int amount) {
		this.item = item;
		this.itemAmount = amount;
	}

	public ItemData Item {
		get {
			return this.item;
		}
		set {
			item = value;
		}
	}

	public int ItemAmount {
		get {
			return this.itemAmount;
		}
		set {
			itemAmount = value;
		}
	}

}
