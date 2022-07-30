using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGun : Gun
{
    public override void Shoot()
    {
        base.Shoot();
    }
    public override void GenerateBullet()
    {   
        PoisonProjectile generatedBullet = Instantiate<PoisonProjectile>(poisonbullet);
        generatedBullet.transform.position = bulletExitPoint.position;
        generatedBullet.transform.rotation = bulletExitPoint.rotation;
        generatedBullet.bulletSpeed = bulletVelocity;
    }
}
