using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BombContainer : MonoBehaviour
{
    private int currBomb;
    private int maxBomb = 3;
    public Image[] bombs;

    public int GetCurrBomb()
    {
        return currBomb;
    }

    public void SetCurrBomb(int bomb)
    {
        this.currBomb = bomb;
    }
    public int GetMaxBomb()
    {
        return maxBomb;
    }



    public void SetMaxBomb(int bomb)
    {
        this.maxBomb = bomb;
    }




    private void Update()
    {
        if (currBomb > maxBomb)
        {
            currBomb = maxBomb;
        }
        for (int i = 0; i < bombs.Length; i++)
        {

            if (i < currBomb)
            {
                bombs[i].enabled = true;
            }
            else
            {
                bombs[i].enabled = false;
            }
        }
    }

}
