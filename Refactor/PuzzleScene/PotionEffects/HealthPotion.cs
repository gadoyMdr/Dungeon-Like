using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Potions/Heal", fileName = "HealEffect")]
public class HealthPotion : PotionEffect
{
    [SerializeField]
    private float heal = 35;  //Base heal

    /// <summary>
    /// Apply an effect
    /// </summary>
    /// <param name="health">The health we want to affect</param>
    public override void ApplyEffect(Health health)
    {
        health.currentHealth += heal; //We Add health
    }

}
