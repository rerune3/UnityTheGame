using UnityEngine;
using System.Collections;

public class ItemData
{

	public enum ItemType
	{
		NONE = -1,
		WEAPON,
		EQUIPMENT,
		POTION,
		QUEST,
		MISCELLANEOUS,
		_SIZE
	}

	public enum WeaponType
	{
		NONE = -1,
		SWORD,
		DAGGER,
		AXE
	}

	public enum EquipmentType
	{
		NONE = -1,
		HEAD,
		BODY,
		ARMS,
		LEGS
	}

	public enum PotionType
	{
		NONE = -1,
		HEALTH,
		STRENGTH,
		OBSERVATION
	}

	private string itemName;
	private int itemID;
	private string itemDescription;
	private bool canEquip;
	private ItemType iType;
	private WeaponType wType;
	private EquipmentType eType;
	private PotionType pType;

	public ItemData() {
		itemName = "";
		itemID = -1;
		itemDescription = "";
		canEquip = false;
		iType = ItemType.NONE;
		wType = WeaponType.NONE;
		eType = EquipmentType.NONE;
		pType = PotionType.NONE;
	}

	public virtual string convertToString ()
	{
		return "Item Name : " + itemName + "\n" +
		"Item ID : " + itemID + "\n" +
		"Item Description : " + itemDescription + "\n" +
		"Can Equip : " + canEquip + "\n" +
		"Item Type : " + iType.ToString () + "\n" +
		"Weapon Type : " + wType.ToString () + "\n" +
		"Equipment Type : " + eType.ToString () + "\n" +
		"Potion Type : " + pType.ToString ();
	}

	public virtual ItemData getCopy ()
	{
		ItemData item = new ItemData ();
		item.CanEquip = canEquip;
		item.ItemDescription = itemDescription;
		item.ItemID = itemID;
		item.ItemName = itemName;
		item.IType = iType;
		item.WType = wType;
		item.EType = eType;
		item.PType = pType;

		return item;
	}

	public string ItemName {
		get {
			return this.itemName;
		}
		set {
			itemName = value;
		}
	}

	public int ItemID {
		get {
			return this.itemID;
		}
		set {
			itemID = value;
		}
	}

	public string ItemDescription {
		get {
			return this.itemDescription;
		}
		set {
			itemDescription = value;
		}
	}

	public bool CanEquip {
		get {
			return this.canEquip;
		}
		set {
			canEquip = value;
		}
	}

	public ItemType IType {
		get {
			return this.iType;
		}
		set {
			iType = value;
		}
	}

	public WeaponType WType {
		get {
			return this.wType;
		}
		set {
			wType = value;
		}
	}

	public EquipmentType EType {
		get {
			return this.eType;
		}
		set {
			eType = value;
		}
	}

	public PotionType PType {
		get {
			return this.pType;
		}
		set {
			pType = value;
		}
	}
}
