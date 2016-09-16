using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JsonObject {

	[System.Serializable]
	public class JsonItem {
		public string itemName;
		public string itemType;
		public string itemSubType;
		public int itemID;
		public string itemDescription;
		public bool canEquip;
	}

	[System.Serializable]
	public class JsonItemsWrapper
	{
		public JsonItem [] items;
	}
}
