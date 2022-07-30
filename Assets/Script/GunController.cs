using UnityEngine;
using System.Collections;
using System;

public class GunController : MonoBehaviour
{
	[Header("Gun Setting")]
	public Transform holdingPoint;
	public Gun[] allGuns;
	Gun theHoldingGun;

	void Start() {
		SwitchGun(0);
	}

	public void SwitchGun(int gunNumber) {
		if (theHoldingGun != null) {
			Destroy(theHoldingGun.gameObject);
		}
		theHoldingGun = Instantiate<Gun>(allGuns[gunNumber]);
		theHoldingGun.transform.position = holdingPoint.position;
		theHoldingGun.transform.rotation = holdingPoint.rotation;
		theHoldingGun.transform.parent = holdingPoint;
	}

	public void Shoot() 
	{
		if (theHoldingGun != null)
		{
			theHoldingGun.Shoot();
		}
	}

	public void Reload() {
		if (theHoldingGun!=null) {
			theHoldingGun.Reload();
		}
	}


	public Gun GetTheHoldingGun() {
		return theHoldingGun;
		
	}
}