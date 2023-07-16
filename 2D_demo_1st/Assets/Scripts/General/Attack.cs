using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("基本数值")]

    public int damage;
    public float attack_range;
    public float attack_rate;

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.GetComponent<Character>()?.take_damage(this);
    }

}
