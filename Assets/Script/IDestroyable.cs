using UnityEngine;

public interface IDestroyable {

	void DeductHealth (int amount);
	void AddDebuff(int status, float statusduration);
		
	
}
