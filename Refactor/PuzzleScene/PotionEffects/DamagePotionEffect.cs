using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Potions/Damage", fileName = "DamageEffect")]
public class DamagePotionEffect : PotionEffect
{
    [SerializeField]
    private float damage = 35;  //Base damage

    /// <summary>
    /// Apply an effect
    /// </summary>
    /// <param name="health">The health we want to affect</param>
    public override void ApplyEffect(Health health)
    {
        health.currentHealth -= damage; //We remove health
    }

}
