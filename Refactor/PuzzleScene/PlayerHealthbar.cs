using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbar : MonoBehaviour
{
    [SerializeField]
    private float healthPerHeart = 25;  //health per heart

    [SerializeField]
    private Image heartImagePrefab; //The prefab of the heart we're gonna instantiate

    private Health playerHealth;    //Reference player's health

    //first callback
    private void Awake()
    {
        playerHealth = GetComponentInParent<Health>();  //Get player's health 
    }

    //After awake but before start
    private void OnEnable()
    {
        playerHealth.HealthModified += UpdateHealthVisuals; //We subscribe to whenever player's health changes
    }

    //Right before Ondestroy
    private void OnDisable()
    {
        playerHealth.HealthModified -= UpdateHealthVisuals; //We unsubscribe
    }

    /// <summary>
    /// Update lifebar
    /// </summary>
    /// <param name="currentHealth">the current player's health</param>
    /// <param name="startHealth">the start health</param>
    void UpdateHealthVisuals(float currentHealth, float startHealth)
    {
        
        //We destroy all hearts
        foreach (Transform trans in transform)
            Destroy(trans.gameObject);

        for (int i = 0; i < Mathf.Ceil(currentHealth / healthPerHeart); i++)    //eg : currentHealth = 76, 76/25 = 3.04, we round it up to 4
            Instantiate(heartImagePrefab, transform);
        
    }


}
