using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public int maxHealth = 100;
    public int currHealth;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currHealth -= 10;
            healthBar.SetHealth(currHealth);
        }

    }
}
