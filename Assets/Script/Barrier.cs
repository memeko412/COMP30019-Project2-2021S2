using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour, IDestroyable
{
    public int barrierHealth;
    private HealthController healthController;
    protected DropController dropController;
    public GameObject blockDestroyEffectPrefb;
    float barrierHeight;
    int blockType;
    bool isDestroyed;

    void Start() {
        healthController = GetComponent<HealthController> ();
        int rangeHealth = UnityEngine.Random.Range(1,barrierHealth);
        healthController.SetMaxHeath(rangeHealth);
        isDestroyed = false;
        dropController = GetComponent<DropController> ();
        dropController.SetItem(Random.Range(0,6));
		switch(dropController.GetItemDrop()) 
		{
			case 1:
				dropController.SetChance(Random.Range(1,200));
				break;
			case 2:
				dropController.SetChance(Random.Range(1,150));
				break;
			case 3:
				dropController.SetChance(Random.Range(1,100));
				break;
			case 4:
				dropController.SetChance(Random.Range(1,50));
				break;
            case 5:
                dropController.SetChance(Random.Range(1,100));
                break;
		}
    }
    
    public void SetBarrier(float height, int blockTypeNumber)
    {
        barrierHeight = height;
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, -height, this.transform.localPosition.z);
        blockType = blockTypeNumber;
        print("block Type is "+blockType);
    }

    public void DeductHealth(int amount) {
		healthController.TakingDamage(amount);
		
		if (healthController.GetHealth() <= 0 && !isDestroyed && blockType==1) {
			DestroyBarrier();
            if(Random.Range(1, 1000) <= dropController.GetDropChance()) {
			    dropController.Drop();
		    }
		}
	}
	public void AddBuff(int status, float statusduration) {
	}

	public void AddDebuff(int status, float statusduration) {
	}

     private void DestroyBarrier() {
        isDestroyed = true;
        GameObject blockDestroyEffectPrefb = Instantiate(this.blockDestroyEffectPrefb);
        blockDestroyEffectPrefb.transform.position = this.transform.position;
        Destroy(blockDestroyEffectPrefb,2f);
        FindObjectOfType<AudioManager>().Play("block");
        GameObject.Destroy (gameObject);
    }
}
