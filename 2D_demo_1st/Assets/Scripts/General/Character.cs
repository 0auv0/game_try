using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("基本属性")]

    public float max_health;
    public float current_health;


    [Header("受伤短暂无敌")]
    public float invincible_time;
    private float invincible_counter;
    public bool is_invincible;

    private void Start()
    {
        current_health = max_health;   
    }

    private void Update()
    {
        if (is_invincible)
        {
            invincible_counter -= Time.deltaTime;
            if(invincible_counter <= 0)
            {
                is_invincible = false;
            }
        }
    }

    public void take_damage(Attack attacker)
    {
        if (is_invincible)
        {
            return;
        }
        else
        {
            if(current_health - attacker.damage > 0)
            {
                current_health -= attacker.damage;
                trigger_invincible();
            }
            else
            {
                current_health = 0;
                //goto death
            }
        }
        
    }

    private void trigger_invincible()
    {
        if (!is_invincible)
        {
            is_invincible = true;
            invincible_counter = invincible_time;
        }
    }
}
