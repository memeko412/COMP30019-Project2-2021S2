using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingObject : MonoBehaviour, IDestroyable {

	public int startingHealth;
	protected HealthController healthController;
	protected DropController dropController;
	protected bool isDead;

	protected virtual void Start() {
		healthController = GetComponent<HealthController> ();
		dropController = GetComponent<DropController> ();
		healthController.SetMaxHeath(startingHealth);
		healthController.SetHealth(startingHealth);
	}

	public virtual void DeductHealth(int amount) {
		healthController.TakingDamage(amount);
		
		if (healthController.GetHealth() <= 0 && !isDead) {
			ObjectDie();
		}
	}

	public virtual void Heal(int potency) {
		healthController.ChangeHealth(potency);
	}

	public virtual void AddMaxHealth(int value) {
		healthController.ChangeMaxHealth(value);
	}

	public virtual void GainShield(int potency) {
		healthController.ChangeArmor(potency);
	}

	public virtual void AddBuff(int status, float statusduration) {
		
	}
	public virtual void AddDebuff(int status, float statusduration) {
		
	}

	public bool IsDead() {
		return healthController.GetHealth() <= 0;
	}

	//protected void ObjectDie() {
	protected virtual void ObjectDie() {
		isDead = true;
		print("Drop rate of this entity is " + dropController.GetDropChance());
		if(Random.Range(1, 1000) <= dropController.GetDropChance()) {
			dropController.Drop();
		}
		//GameObject.Destroy (gameObject);
	}

	protected void Drop() {
		//TODO Make a drop controller that's created on entity creation that decides
		//what the entity will drop on death
	}
}
