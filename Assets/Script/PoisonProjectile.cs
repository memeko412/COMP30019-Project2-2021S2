using UnityEngine;
using System.Collections;
using System;


public class PoisonProjectile : MonoBehaviour {

	public LayerMask layer;
	public float bulletSpeed = 10;
	public int hittingDamge;

	void Start() {
		CheckInsideCollision();
	}

    public void SetSpeed(float _bulletSpeed) {
		bulletSpeed = _bulletSpeed;
	}
	
	void Update () {
		float distanceTravelled = bulletSpeed * Time.deltaTime;

		//detecting hitting object using ray cast, learned from the tutorial on youtube:
		//https://www.youtube.com/watch?v=0jTPKz3ga4w
		//Also the documentation: https://docs.unity3d.com/2020.1/Documentation/ScriptReference/Physics.Raycast.html
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;
		if(Physics.Raycast(ray,out hit,distanceTravelled,layer,QueryTriggerInteraction.Collide))
        {
			IDestroyable hitObject = hit.collider.GetComponent<IDestroyable>();
			if(hitObject!=null)
			{
				hitObject.AddDebuff(1,5);
				hitObject.DeductHealth(hittingDamge);
			}
			Destroy(this.gameObject);
		}

		transform.Translate (Vector3.forward *distanceTravelled);
	}
	//documentation about overlapSphere: https://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html
	void CheckInsideCollision() {
		Collider[] collisionsInsideObject = Physics.OverlapSphere(this.transform.position,0.08f,layer);
		if (collisionsInsideObject.Length!=0) {
			IDestroyable insideHitObject = collisionsInsideObject[0].GetComponent<IDestroyable>();
			if(insideHitObject!=null)
			{
				insideHitObject.DeductHealth(hittingDamge);
			}
			Destroy(gameObject);
		}
	}
   
}
