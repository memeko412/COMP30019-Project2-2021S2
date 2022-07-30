using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Replicant : Enemy
{
    public Boss_Replicant replicant;

    // Update is called once per frame
    
    protected override void ObjectDie()
    {
        if(startingHealth != 1 )
        {
            FindObjectOfType<AudioManager>().Play("block");
            Boss_Replicant son1 = Instantiate<Boss_Replicant>(replicant);
            son1.transform.position = this.transform.position;
            son1.transform.rotation = this.transform.rotation;
            son1.transform.localScale = this.transform.localScale - new Vector3(0.5f, 0.5f, 0.5f);
            son1.startingHealth = this.startingHealth - 1;
            Boss_Replicant son2 = Instantiate<Boss_Replicant>(replicant);
            son2.transform.position = this.transform.position+new Vector3(2,0,0);
            son2.transform.rotation = this.transform.rotation;
            son2.transform.localScale = this.transform.localScale - new Vector3(0.5f, 0.5f, 0.5f);
            son2.startingHealth = this.startingHealth - 1;
        }

        if (isDead && deadTime <= 1)
        {
            deadTime += Time.deltaTime * dieSpeed;
            //Assign a value to the material
            _material.SetFloat("_ClipAmount", deadTime);

            if (deadTime > 1) GameObject.Destroy(gameObject);
        }
        base.ObjectDie();
    }
    
}
