using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour
{
    protected int item;
    protected float dropChance;  //between 0-1000, 1000 = 100%
    
    // Start is called before the first frame update
    void Start()
    {
 
    }

    public void SetItem(int dropitem){
        this.item = dropitem;
    }
    public void SetChance(float chance){
        this.dropChance = chance;
    }


    public void Drop() {
        print("Dropped item" + item + "!");
        Transform player = GameObject.FindGameObjectWithTag ("Player").transform;
        Player playerEntity = player.GetComponent<Player> ();
        switch (item)
        {
            case 1:
                playerEntity.Heal(1);
                break;
            case 2:
                playerEntity.AddMaxHealth(1);
                playerEntity.Heal(1);  
                break;
            case 3:
                playerEntity.GainShield(1);
                break;
            case 4:
                playerEntity.AddBuff(2, 10);
                break;
            case 5:
                playerEntity.grenadeController.AddGrenade(1);
                break;
        }

    }

    public float GetDropChance () {
        return dropChance;
    }

    public int GetItemDrop() {
        return item;
    }
}
