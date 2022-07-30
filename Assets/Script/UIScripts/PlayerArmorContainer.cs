using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerArmorContainer : MonoBehaviour
{
    private int currArmor;
    private int maxArmor = 5;
    public Image[] armors;

    public int GetCurrArmor()
    {
        return currArmor;
    }

    public void SetCurrArmor(int armor)
    {
        this.currArmor = armor;
    }
    public int GetMaxArmor()
    {
        return maxArmor;
    }



    public void SetMaxArmor(int armor)
    {
        this.maxArmor = armor;
    }




    private void Update()
    {
        if(currArmor > maxArmor)
        {
            currArmor = maxArmor;
        }
        for (int i = 0; i < armors.Length; i++)
        {
           
            if (i < currArmor)
            {
                armors[i].enabled = true;
            }
            else
            {
                armors[i].enabled = false;
            }
        }
    }

}
