using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gun : MonoBehaviour
{
	public Transform bulletExitPoint;
	[Header("Bullet Setting")]
	public Projectile bullet;
	public PoisonProjectile poisonbullet;
	public float shootingFreq = 90;
	public float bulletVelocity = 25;
	public int bulletNumber;      
	protected float nextFireTime;
	private bool shootable = true;
	private int remainBullet;

	void Start() {
		remainBullet = bulletNumber;
	}
    public virtual void GenerateBullet()
    {
		
	}

	void Update() {
		if (remainBullet <=0) {
			Reload();
		}
	}
	public virtual void Shoot()
	{
        if (remainBullet > 0 && shootable && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + shootingFreq / 1000;
			remainBullet-=1;
			FindObjectOfType<AudioManager>().Play("gun");
            GenerateBullet();
        }
	}

	public void Reload()
    {
		if(remainBullet == bulletNumber)
        {
			return;
        }
		if (shootable)
		{
			shootable = false;
			Invoke("Reloading", 1f);
		}
    }

	private void Reloading()
    {
		remainBullet = bulletNumber;
		shootable = true;
	}

	public int GetRemainBullet() {
		return remainBullet;
	}

}
