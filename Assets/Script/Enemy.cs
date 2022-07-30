using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyDieEvent : UnityEvent<Enemy> { }
public class Enemy : LivingObject
{
	protected NavMeshAgent navMensh;
	protected Transform playerTransform;
	protected LivingObject player;
	protected GunController gunController;


	public GameObject enemyDeathEffectPrefb;
	public float updateRate =0.2f;
	public int collidingDamage = 1;
	public int specialAbility;

	public float fireRate = 1f;
	protected float shootCounting = 0f;
    public EnemyDieEvent onEnemyDie = new EnemyDieEvent();

	#region Shader Part

	//Material declaration
    protected Material _material = null;

    public float dieSpeed = .5f;
    public float deadTime = 0f;

	#endregion
	
	protected override void Start () {
		base.Start ();
		navMensh = GetComponent<NavMeshAgent> ();
		if (GameObject.FindGameObjectWithTag ("Player") != null) {

			
			playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
			player = playerTransform.GetComponent<LivingObject> ();
			
		}

		healthController = GetComponent<HealthController> ();
		dropController = GetComponent<DropController> ();
		gunController = GetComponent<GunController> ();
		dropController.SetItem(Random.Range(0,6));
		switch(dropController.GetItemDrop()) 
		{
			case 1:
				dropController.SetChance(Random.Range(1,600));
				break;
			case 2:
				dropController.SetChance(Random.Range(1,300));
				break;
			case 3:
				dropController.SetChance(Random.Range(1,400));
				break;
			case 4:
				dropController.SetChance(Random.Range(1,100));
				break;
			case 5:
				dropController.SetChance(Random.Range(1,300));
				break;
		}
		print("Drop rate of this enemy is: " + dropController.GetDropChance());
		specialAbility = Random.Range(1,10);

		move();

		_material = this.GetComponent<MeshRenderer>().materials[0];

	}
	protected virtual void move()
    {
		if (playerTransform != null)
		{
			StartCoroutine(FindPlayer());
		}
	
	}

	



	public override void DeductHealth(int amount) {
		if(healthController) {
			if (amount >= healthController.GetHealth()) {
				onEnemyDie.Invoke(this);
				GameObject enemyDeathEffect = Instantiate(this.enemyDeathEffectPrefb);
				enemyDeathEffect.transform.position = this.transform.position;
				Destroy(enemyDeathEffect,2f);
			}
			base.DeductHealth(amount);
		}
	}

	void OnTriggerEnter ( Collider obj) {
		if (obj.tag == "Player") {
			player.DeductHealth(collidingDamage);
		}
	}
	
	void Update () {
		if (gunController!=null) {
			if (shootCounting<=0f) {
				gunController.Shoot();
				shootCounting = 1f/fireRate;
			}
			shootCounting-= Time.deltaTime;
		}

		//Dissolve Control
        if (isDead && deadTime <=1)
        {
            deadTime += Time.deltaTime * dieSpeed ;
			//Assign a value to the material
            _material.SetFloat("_ClipAmount", deadTime);

            if (deadTime > 1) GameObject.Destroy(gameObject);
        }
		
	}

	IEnumerator FindPlayer() {
		while ( playerTransform !=null&& !isDead) {

			Vector3 pos = new Vector3(playerTransform.position.x, 0, playerTransform.position.z);
        	navMensh.SetDestination(pos);
        	yield return new WaitForSeconds(updateRate);
			
		}
	}
	
}
