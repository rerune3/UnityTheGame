using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseCharacter {
	public enum StatType {
		STRENGTH, DEFENSE, STAMINA, INTELLIGENCE, OBSERVATION, _SIZE
	}

	private int [] stats;
	private string charName;
	private int MAX_HEALTH = 100;
	private int health;

	public BaseCharacter () {
		stats = new int[(int)StatType._SIZE]; 
		charName = "";
		health = MAX_HEALTH;
	}

	public void damage(int damageAmount) {
		damageAmount = (damageAmount < health) ? damageAmount : health;
		health -= damageAmount;
	}

	public void heal(int healAmount) {
		healAmount = (MAX_HEALTH - health) > healAmount ? healAmount : MAX_HEALTH - health;
		health += healAmount;
	}

	public void SetStat(int stat, int value) {
		stats [stat] = value;
	}

	public int [] GetStats() {
		return stats;
	}

	public string CharName {
		get {
			return this.charName;
		}
		set {
			charName = value;
		}
	}
	
}
