using UnityEngine;
using System.Collections;

public class NPCEnemyLogic : MonoBehaviour {

	private NPCEnemy enemy;
	private GameObject enemyObject;
	private CircleCollider2D combatCollider;
	private NPCMovement movementComponent;
	private float attackHoldTime;
	private bool inAttackMode;
	private bool canAttack;

	void Start() {
		enemy = new NPCEnemy ();
		enemyObject = this.gameObject;
		movementComponent = enemyObject.GetComponent<NPCMovement> ();
		combatCollider = enemyObject.AddComponent<CircleCollider2D> ();
		combatCollider.isTrigger = true;
		combatCollider.radius = enemy.NonCombatRange;
		inAttackMode = false;
		canAttack = false;
		attackHoldTime = 2;
	}

	void Update() {
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log ("Enemy is attacking");
			Utilities.displayText ("You are too close to an enemy! You may be attacked!");
			inAttackMode = true;
			combatCollider.radius = enemy.CombatRange;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log ("Why're you running son?");
			Utilities.displayText ("It looks like you got away from the enemy.");
			inAttackMode = false;
			combatCollider.radius = enemy.CombatRange;
		}
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Player" && inAttackMode) {
			Debug.Log ("Enemy Attack!");
			canAttack = true;
			StartCoroutine ("attackTarget", other.gameObject);
		}
	}

	void OnCollisionExit2D (Collision2D other) {
		if (other.gameObject.tag == "Player" && inAttackMode) {
			Debug.Log ("Not close enough to physically attack!");
			canAttack = false;
			StopCoroutine ("attackTarget");
		}
	}

	IEnumerator attackTarget(GameObject target) {
		while (true) {
			if (canAttack) {
				target.SendMessage ("damagePlayer", 10);
				Debug.Log ("You've been hit!");
				Utilities.displayText ("You've been hit!");
				yield return new WaitForSeconds (attackHoldTime);
			}
			if (!inAttackMode)
				break;
		}
	}
}
