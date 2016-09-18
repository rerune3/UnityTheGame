using UnityEngine;
using System.Collections;

public class Utilities : MonoBehaviour {

	private static TextDisplayLogic textDisplayLogic;
	private static Vector3[] directions;

	void Awake() {
		
	}

	void Start() {
		textDisplayLogic = GameObject.Find("TextDisplayPanel").GetComponent<TextDisplayLogic>();
	}

	public static GameObject instantiate (string key, Transform transform, bool isChild) {
		GameObject obj = (GameObject)
			Instantiate (ItemBankWrapper.getItemObject (key), transform.position, transform.rotation);
		obj.name = key;
		if (isChild)
			obj.transform.parent = transform;
		return obj;
	}

	public static GameObject instantiateEmpty (Transform transform, bool isChild) {
		GameObject obj = (GameObject)
			Instantiate (Resources.Load("Prefabs/Scapegoat"), transform.position, transform.rotation);
		if (isChild)
			obj.transform.parent = transform;
		return obj;
	}

	public static GameObject instantiateMinorPrefab (string key, Transform parent, Vector3 pos, bool isChild) {
		GameObject obj = (GameObject)
			Instantiate (Resources.Load("Prefabs/" + key), pos, parent.rotation);
		if (isChild) {
			obj.transform.parent = parent;
			obj.transform.localPosition = pos;
		}
		return obj;
	}

	public static Vector3 getDirectionToFace (Vector3 origin, Vector3 target) {
		Vector3 delta = origin - target;
		if (Mathf.Abs (delta.x) > Mathf.Abs (delta.y)) {
			delta.x = 0;
			return delta.y < 0 ? Vector3.down : Vector3.up;
		} else {
			delta.y = 0;
			return delta.x < 0 ? Vector3.left : Vector3.right;
		}
	}

	public static Vector3 furtherNormalizeDirection (Vector3 dir) {
		if (Mathf.Abs (dir.x) > Mathf.Abs (dir.y)) {
			return new Vector3 (dir.x, 0);
		} else {
			return new Vector3 (0, dir.y);
		}
	}

	public static Vector3 getDisplacement (Vector3 origin, Vector3 target) {
		return target - origin;
	}

    public static void PrintVectors(Vector3[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            Debug.Log(arr[i]);
        }
    }

	public static void DrawBounds(Bounds bounds) {
		Vector3 topRight = bounds.max;
		Vector3 topLeft = topRight - new Vector3 (bounds.extents.x * 2, 0);
		Vector3 bottomRight = topRight - new Vector3(0, bounds.extents.y * 2);
		Vector3 bottomLeft = bottomRight - new Vector3 (bounds.extents.x * 2, 0);

		Debug.DrawLine (topLeft, topRight, Color.red, Mathf.Infinity);
		Debug.DrawLine (topLeft, bottomLeft, Color.red, Mathf.Infinity);
		Debug.DrawLine (bottomRight, bottomLeft, Color.red, Mathf.Infinity);
		Debug.DrawLine (bottomRight, topRight, Color.red, Mathf.Infinity);
	}

	public static Vector3 GetRandomDirection() {
		int randomNumber = Random.Range (0, 9999) % 4;

		if (randomNumber == 0) {
			return Vector3.up;
		} else if (randomNumber == 1) {
			return Vector3.down;
		} else if (randomNumber == 2) {
			return Vector3.right;
		} else {
			return Vector3.left;
		}
	}

	public static void displayText (string text) {
		showTextDisplay ();
		textDisplayLogic.displayText (text);
	}

	public static void showTextDisplay() {
		textDisplayLogic.showTextDisplayPanel ();
	}

	public static void hideTextDisplay() {
		textDisplayLogic.hideTextDisplayPanel ();
	}
}
