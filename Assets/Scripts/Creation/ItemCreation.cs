using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ItemCreation : MonoBehaviour
{

	public static bool ready;

	void Awake() {
		ItemBankWrapper.instantiateItemBank ();
		createItems ();
	}

	void Start ()
	{
		
	}

	public void createItems ()
	{
		TextAsset jsonStr = Resources.Load<TextAsset> ("items");
		JsonObject.JsonItem[] items = getItemsFromJson (jsonStr.text);
		populateItemBank (items);
	}

	private void populateItemBank (JsonObject.JsonItem[] items)
	{
		foreach (JsonObject.JsonItem item in items) {
			ItemData newItem = new ItemData ();
			if (item.itemType.Equals ("WEAPON")) {
				newItem.WType = (ItemData.WeaponType)Enum.Parse (typeof(ItemData.WeaponType), item.itemSubType);
				newItem.CanEquip = true;
			} else if (item.itemType.Equals ("EQUIPMENT")) {
				newItem.EType = (ItemData.EquipmentType)Enum.Parse (typeof(ItemData.EquipmentType), item.itemSubType);
				newItem.CanEquip = true;
			} 

			newItem.IType = (ItemData.ItemType)Enum.Parse (typeof(ItemData.ItemType), item.itemType);
			newItem.ItemName = item.itemName;
			newItem.ItemDescription = item.itemDescription;
			ItemBankWrapper.addToItemBank (newItem.ItemName, newItem);
		}
	}

	private JsonObject.JsonItem [] getItemsFromJson (string jStr)
	{
		JsonObject.JsonItemsWrapper wrapper = JsonUtility.FromJson<JsonObject.JsonItemsWrapper> (jStr);
		return wrapper.items;
	}
}
