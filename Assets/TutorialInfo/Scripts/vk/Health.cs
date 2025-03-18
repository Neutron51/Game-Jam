using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     health = maxHealth;
    //     //TakeDamage(1);

    // }
    public virtual void TakeDamage(int amount)
    {
        health = Mathf.Clamp(health - amount, 0, maxHealth);
        if (health == 0)
        {
            Death();
        }
    }
    public virtual void Death()
    {
        Debug.Log("Dead");
    }
}
