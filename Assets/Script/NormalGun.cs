using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGun : Gun
{
    public override void Shoot()
    {
        base.Shoot();
    }
    public override void GenerateBullet()
    {   
        Projectile generatedBullet = Instantiate<Projectile>(bullet);
        generatedBullet.transform.position = bulletExitPoint.position;
        generatedBullet.transform.rotation = bulletExitPoint.rotation;
        generatedBullet.bulletSpeed = bulletVelocity;
    }
}
