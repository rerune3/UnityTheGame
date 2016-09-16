using UnityEngine;
using System.Collections;

public class PlayerGeneralLogic : MonoBehaviour {

	private int MAX_HEALTH = 100;
	private int health;
	private int money;
	private GameObject player;

	// Use this for initialization
	void Start () {
		health = MAX_HEALTH;
		money = 10;
		player = gameObject;
	}

	public void damagePlayer(int damageAmount) {
		damageAmount = (damageAmount < health) ? damageAmount : health;
		health -= damageAmount;
	}

	public void healPlayer(int healAmount) {
		healAmount = (MAX_HEALTH - health) > healAmount ? healAmount : MAX_HEALTH - health;
		health += healAmount;
	}

}
