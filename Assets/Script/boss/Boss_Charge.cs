using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Charge : Enemy
{
	int i = 0;
	Vector3 target;
	public GameObject obj;
	GameObject tp;
	public GrenadeControllerEnemy grenade;
    // Update is called once per frame

    private void Start()
    {
		base.Start();
		
		
    }
    protected override void move()
	{
		//StartCoroutine(FindPlayer());
	}
	void Update()
	{
		if (i == 800 )
		{
			targeting();
		}
		
		
        
		grenades(i);

		if (i > 1200)
        {
			if(tp!=null)
			Destroy(tp);
			i = 0;
        }
		charging();
		i++;
		if (isDead && deadTime <=1)
        {
            deadTime += Time.deltaTime * dieSpeed ;
			//Assign a value to the material
            _material.SetFloat("_ClipAmount", deadTime);

            if (deadTime > 1) GameObject.Destroy(gameObject);
        }
	}

	void charging()
	{
		if(i==1000)
		if(tp != null) {
			navMensh.SetDestination(target);
		}
	}
	protected override void ObjectDie()
    {
        base.ObjectDie();
		if(tp!= null)
        {
			Destroy(tp);
        }
    }


    void targeting()
	{
		tp = Instantiate<GameObject>(obj);
		FindObjectOfType<AudioManager>().Play("target");
		target = new Vector3(playerTransform.position.x, 0, playerTransform.position.z);
		tp.transform.position = target;

	}

	void grenades(int i)
    {
		i += 20;
		if (i % 490 == 0)
		{
			Vector3 throwpoint = new Vector3(player.transform.position.x - 1, player.transform.position.y,
										player.transform.position.z - 1);
			grenade.ThrowTheGrenade(throwpoint);
		}
		if (i % 500 == 0)
		{
			Vector3 throwpoint = new Vector3(player.transform.position.x, player.transform.position.y,
										player.transform.position.z - 1);
			grenade.ThrowTheGrenade(throwpoint);
		}
		if (i % 510 == 0)
		{
			Vector3 throwpoint = new Vector3(player.transform.position.x + 1, player.transform.position.y,
										player.transform.position.z - 1);
			grenade.ThrowTheGrenade(throwpoint);
		}

		if (i % 520 == 0)
		{
			Vector3 throwpoint = new Vector3(player.transform.position.x-1, player.transform.position.y,
										player.transform.position.z);
			grenade.ThrowTheGrenade(throwpoint);
		}
		if (i % 530 == 0)
		{
			Vector3 throwpoint = new Vector3(player.transform.position.x, player.transform.position.y,
										player.transform.position.z);
			grenade.ThrowTheGrenade(throwpoint);
		}
		if (i % 540 == 0)
		{
			Vector3 throwpoint = new Vector3(player.transform.position.x+1, player.transform.position.y,
										player.transform.position.z);
			grenade.ThrowTheGrenade(throwpoint);
		}

		if (i % 550 == 0)
		{
			Vector3 throwpoint = new Vector3(player.transform.position.x-1, player.transform.position.y,
										player.transform.position.z+1);
			grenade.ThrowTheGrenade(throwpoint);
		}
		if (i % 560 == 0)
		{
			Vector3 throwpoint = new Vector3(player.transform.position.x, player.transform.position.y,
										player.transform.position.z+1);
			grenade.ThrowTheGrenade(throwpoint);
		}
		if (i % 570 == 0)
		{
			Vector3 throwpoint = new Vector3(player.transform.position.x+1, player.transform.position.y,
										player.transform.position.z+1);
			grenade.ThrowTheGrenade(throwpoint);
		}



	}

	IEnumerator FindPlayer()
	{
	
		while (tp != null && !isDead)
		{
			navMensh.SetDestination(target);
			yield return new WaitForSeconds(updateRate);

		}
	}
}
