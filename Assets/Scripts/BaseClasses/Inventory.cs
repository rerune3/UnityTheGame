using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory {

	private Dictionary<ItemData.ItemType, List<InventoryItem>> itemInventory;
	public int MAX_ITEMS = 5;
	private int itemCount = 0;

	public Inventory() {
		itemInventory = new Dictionary<ItemData.ItemType, List<InventoryItem>> ();
	}

	public void addItemToInventory (ItemData item) {
		if (itemCount == MAX_ITEMS) {
			Debug.Log ("Inventory is full");
		} else if (!itemInventory.ContainsKey (item.IType)) {
			itemInventory.Add (item.IType, null);
			itemInventory [item.IType] = new List<InventoryItem> ();
			addItemToInventory (item);
		} else if (!itemExistsInInventory(item)) {
			itemInventory [item.IType].Add (new InventoryItem(item.getCopy (), 1));
			itemCount++;
		}
	}

	private bool itemExistsInInventory (ItemData item) {
		foreach (InventoryItem i in itemInventory[item.IType]) {
			if (item.ItemName.Equals (i.Item.ItemName)) {
				i.ItemAmount++;
				itemCount++;
				return true;
			}
		}
		return false;
	}

	public void removeItemFromInventory (ItemData item) {
		for (int i = 0; i < itemInventory[item.IType].Count; i++) {
			if (item.ItemName.Equals (itemInventory[item.IType][i].Item.ItemName)) {
				itemInventory[item.IType][i].ItemAmount--;
				itemCount--;
				if (itemInventory[item.IType][i].ItemAmount == 0) {
					itemInventory [item.IType].RemoveAt(i);
				}
				break;
			}
		}
	}

	public void printInventory () {
		Debug.Log ("Inventory Size: " + itemCount);
		foreach (ItemData.ItemType iType in itemInventory.Keys) {
			Debug.Log (iType.ToString () + "S:" + "\n\n");
			foreach (InventoryItem i in itemInventory[iType]) {
				Debug.Log (i.Item.ItemName + " : " + i.ItemAmount + "\n");
			}
			Debug.Log ("\n");
		}
	}
}
