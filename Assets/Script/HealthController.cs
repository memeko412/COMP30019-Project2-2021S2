using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Manages health and armor of entities
public class HealthController : MonoBehaviour
{
    public const int MAX_ARMOR = 5;
    protected int health;
    protected int maxhealth;
    protected int armor;
    protected bool isInvunerable;
    // Start is called before the first frame update
    void Start()
    {
        //this.maxhealth = 5;
        //this.health = maxhealth;
        this.armor = 0;
        this.isInvunerable = false;
    }

    // Set health
    public void SetHealth(int health) {
        this.health = health;
        if(this.health > this.maxhealth) {
            this.health = this.maxhealth;
        } else if (this.health < 0) {
            this.health = 0;
        }     
    }

    // Set armor
    public void SetArmor(int armor) {
        this.armor = armor;
        if(this.armor > MAX_ARMOR) {
            this.armor = MAX_ARMOR;
        } else if (this.armor < 0) {
            this.armor = 0;
        }
    }

    // Set max health
    public void SetMaxHeath(int value) {
        this.maxhealth = value;
        if(this.maxhealth < 0) {
            this.maxhealth = 0;
        }
        if(this.maxhealth > this.health) {
            this.health = this.maxhealth;
        }

    }

    // Increase/Decrease health
    public void ChangeHealth(int value) {
        this.health += value;
        if(this.health > this.maxhealth) {
            this.health = this.maxhealth;
        }
        if(this.health < 0) {
            this.health = 0;
        }
        
    }
    // Increase/Decrease armor
    public void ChangeArmor(int value) {
        this.armor += value;
        if(this.armor > MAX_ARMOR) {
            this.armor = MAX_ARMOR;
        }
        if(this.armor < 0) {
            this.armor = 0;
        }
    }

    // Increase/Decrease Max Health
    public void ChangeMaxHealth(int value) {
        this.maxhealth += value;
        if(this.maxhealth < 0) {
            this.maxhealth = 0;
        }
    }

    // Taking damage 
    public void TakingDamage(int value) {
        if(this.isInvunerable) value = 0;
        if(this.armor > 0 && value > 0) {
            if(this.armor > value) {
                this.armor -= value;
                value = 0;
            } else {
                value -= this.armor;
                this.armor = 0;
            }
        }
        this.health -= value;
    }

    public void SetInvunerable(bool invun) {
        this.isInvunerable = invun;
    }


    public int GetHealth() {
        return this.health;
    }

    public int GetMaxHealth() {
        return this.maxhealth;
    }

    public int GetArmor() {
        return this.armor;
    }

    public bool GetisInvunerable() {
        return this.isInvunerable;
    }

}
