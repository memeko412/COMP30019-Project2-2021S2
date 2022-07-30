using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (GunController))]
public class Player : LivingObject {
	public UnityEvent playerIsDead;
	Camera topCamera;
	GunController gunController;
	public PlayerHeartContainer heartContainer;
	public PlayerArmorContainer armorContainer;
	public GrenadeController grenadeController;
	StatusController statusController;
	private Animator animator;
	
	protected override void Start () {
		base.Start ();
		gunController = GetComponent<GunController> ();
		grenadeController = GetComponent<GrenadeController> ();
		healthController.SetArmor(3);
		statusController = GetComponent<StatusController> ();
		animator = GetComponent<Animator>();
		topCamera = Camera.main;
		print("Hashcode of player is " + this.GetHashCode());
	}

	void FixedUpdate () {
		// Status effects
		GetAllStatusResults();
		if (healthController.GetHealth() <= 0 && !isDead) {
			ObjectDie();
		}
		if (this.transform.position.y<-30) {
			this.playerIsDead.Invoke();
		}

		if (gunController.GetTheHoldingGun()) {
			if (Input.GetMouseButton(0)) {
				animator.SetBool("isShoot",true);
				gunController.Shoot();
			}
			else if(!Input.GetMouseButton(0)) {
				animator.SetBool("isShoot",false);
			}
		}
		//swtich guns
		for (int i=0;i<gunController.allGuns.Length;i++) {
			if (Input.GetKeyDown((i+1)+"") || Input.GetKeyDown("["+(i+1)+"]")) {
				gunController.SwitchGun(i);
				break;
			}
		}
		//UI
		armorContainer.SetCurrArmor(healthController.GetArmor());
		heartContainer.SetCurrHealth(healthController.GetHealth());
		heartContainer.SetMaxHealth(healthController.GetMaxHealth());
	}

	public override void DeductHealth(int amount) {
		if (amount >= healthController.GetHealth()) {
			this.playerIsDead.Invoke();
		}
		base.DeductHealth(amount);
	}	


	public override void AddBuff(int status, float statusduration) {
		statusController.AddBuff(status, statusduration);
	}

	public override void AddDebuff(int status, float statusduration) {
		statusController.AddDebuff(status,statusduration);
	}

    private void OnDestroy()
    {
		armorContainer.SetCurrArmor(healthController.GetArmor());
		heartContainer.SetCurrHealth(healthController.GetHealth());
		heartContainer.SetMaxHealth(healthController.GetMaxHealth());
		
	}



	protected void GetAllStatusResults() {
		switch (statusController.GetBuffEffect()) 
		{
			case -1:
				// Remove status
				statusController.RemoveBuff();
				heartContainer.SetStatus(0);
				break;
			case 0:
				// Do nothing
				break;
			case 2:
				// invun
				heartContainer.SetStatus(1);
				if (!healthController.GetisInvunerable()) {
					healthController.SetInvunerable(true);
				}
				break;
			case -2:
				heartContainer.SetStatus(0);
				healthController.SetInvunerable(false);
				statusController.RemoveBuff();
				break;
		}
		switch(statusController.GetDebuffEffect()) 
		{
			case -1:
				// Remove status
				heartContainer.SetStatus(0);
				statusController.RemoveDebuff();
				break;
			case 0:
				// Do nothing
				break;
			case 1:
				// Poison Damage
				heartContainer.SetStatus(-1);
				healthController.ChangeHealth(-1);
				break;
		}

	}
}
