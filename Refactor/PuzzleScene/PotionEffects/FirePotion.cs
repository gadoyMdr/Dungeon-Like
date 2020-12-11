using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Potions/Fire", fileName = "FireEffect")]
public class FirePotion : PotionEffect
{
    [SerializeField]
    private float damage = 10;  //Base damage

    [SerializeField]
    private float timeBetweenDamage = 2;

    [SerializeField]
    private int tickAmount = 8;

    /// <summary>
    /// Apply an effect
    /// </summary>
    /// <param name="health">The health we want to affect</param>
    public override void ApplyEffect(Health health)
    {
        health.GetComponent<MonoBehaviour>().StartCoroutine(FireCoroutine(health)); //The coroutine will stop when the entity dies
    }

    IEnumerator FireCoroutine(Health health)
    {
        for (int i = 0; i < tickAmount; i++)
        {
            health.currentHealth -= damage;
            yield return new WaitForSeconds(timeBetweenDamage);
        }
    }

}
