using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemBankWrapper {

	private static Dictionary<string, ItemData> itemDataBank;
	private static Dictionary<string, GameObject> itemObjectBank;
	private static List<string> keys;

	public static void instantiateItemBank () {
		itemDataBank = new Dictionary<string, ItemData> ();
		itemObjectBank = new Dictionary<string, GameObject> ();
		keys = new List<string> ();
	}

	public static void addToItemBank (string key, ItemData value) {
		itemDataBank.Add (key, value);
		itemObjectBank.Add(key, (GameObject) 
			Resources.Load("Prefabs/" + value.IType.ToString() + "/" + key));
		itemObjectBank [key].tag = "Item";
		keys.Add (key);
	}

	public static bool itemExists (string key) {
		return itemDataBank.ContainsKey (key);
	}

	public static ItemData getItemData (string key) {
		return itemDataBank.ContainsKey (key) ? itemDataBank [key].getCopy() : null;
	}

	public static GameObject getItemObject (string key) {
		return itemObjectBank.ContainsKey (key) ? itemObjectBank [key] : null;
	}

	public static string getRandomKey() {
		return keys [Random.Range (0, keys.Count)];
	}

	public static void printItemBank() {
		foreach (string itemName in itemDataBank.Keys) {
			Debug.Log (itemDataBank[itemName].convertToString() + "\n\n");
		}
	}
}
