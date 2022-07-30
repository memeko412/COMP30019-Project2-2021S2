using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHeartContainer : MonoBehaviour
{
    private int currHealth;
    private int maxHealth;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite poisonHeart;
    public Sprite invincibableHeart;
    private Sprite status;
    private void Start()
    {
        status = fullHeart;
    }
    public int GetCurrHealth()
    {
        return currHealth;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth =maxHealth;
    }
    public void SetCurrHealth(int health)
    {
        this.currHealth = health;
    }

    //0 normal, -1 poision, 1 invul
    public void SetStatus(int state)
    {
        if(state == 0)
        {
            status = fullHeart;
        }else if(state == 1)
        {
            status = invincibableHeart;
        }else if (state == -1)
        {
            status = poisonHeart;
        }
    }


    private void Update()
    {
        if(currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i< currHealth)
            {
                hearts[i].sprite = status;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

}

