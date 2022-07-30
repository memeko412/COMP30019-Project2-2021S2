using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Controller which holds buffs and debuffs an entity currently have
// can only hold one buff and one debuff at the same time
public class StatusController : MonoBehaviour
{
    protected Status buff;
    protected Status debuff;
    // Start is called before the first frame update
    void Start()
    {
        buff = gameObject.AddComponent<Status> ();
        debuff = gameObject.AddComponent<Status> ();
    }

    public void AddBuff(int buffID, float duration) {
        if(this.buff.CanSetStatus()) {
            buff.SetStatus(buffID, duration);
        }
    }

    public void AddDebuff(int debuffID, float duration) {
        if(this.debuff.CanSetStatus()) {
            debuff.SetStatus(debuffID, duration);
        }
    }

    public void RemoveBuff() {
        buff.SetStatus(0, 0);
    }

    public void RemoveDebuff() {
        debuff.SetStatus(0, 0);
    }

    public int GetBuffEffect() {
        if(buff != null) {
            return buff.StatusEffect();
        }
        return 0;
    } 

    public int GetDebuffEffect() {
        if(debuff != null) {
            return debuff.StatusEffect();  
        }
        return 0;
    }

}
