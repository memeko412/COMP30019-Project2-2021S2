using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss1 : Enemy
{

	LivingObject boss;
	int i = 0;
	Vector3 target;
	public GameObject obj;
	GameObject tp;

    
    protected override void move()
    {
        
    }

    void Update()
	{
		if (isDead && deadTime <=1)
        {
            deadTime += Time.deltaTime * dieSpeed ;
			//Assign a value to the material
            _material.SetFloat("_ClipAmount", deadTime);

            if (deadTime > 1) GameObject.Destroy(gameObject);
        }
		if (gunController != null)
		{
            
			if (shootCounting <= 0f)
			{
				this.transform.Rotate(transform.up, 10);
				gunController.Shoot();
				shootCounting = 1f / fireRate;
			}
			shootCounting -= Time.deltaTime;
		}

		if(i%600== 0)
        {
			targeting();
			
        }
		if (i % 900 == 0)
		{
			flash();
			i = 0;
		}
		i++;
	}

	void flash()
    {
		FindObjectOfType<AudioManager>().Play("flash");
		if (player!= null)
        {
			this.transform.position = target;
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


	protected override void ObjectDie()
	{
		base.ObjectDie();
		if (tp != null)
		{
			Destroy(tp);
		}
	}



}
