using UnityEngine;
using System.Collections;

public class NPCEnemy : BaseCharacter{

	public enum CombatTactic {
		AUTO, RESERVED, AGGRESSIVE
	};

	private CombatTactic tactic;
	private float nonCombatRange;
	private float combatRange;
	private float speed;

	public NPCEnemy () {
		tactic = CombatTactic.AGGRESSIVE;
		nonCombatRange = 1;
		combatRange = 2;
	}

	public CombatTactic Tactic {
		get {
			return this.tactic;
		}
		set {
			tactic = value;
		}
	}

	public float NonCombatRange {
		get {
			return this.nonCombatRange;
		}
		set {
			nonCombatRange = value;
		}
	}

	public float CombatRange {
		get {
			return this.combatRange;
		}
		set {
			combatRange = value;
		}
	}
}
