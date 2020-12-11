using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script that takes care of the entity health
/// </summary>
public class Health : MonoBehaviour
{
    public Action<float, float> HealthModified; //Fired with CurrentHealth and StartHealth to get a value < 0
    public Action<float> HealthChanged; //Fired with the amount of life changed;

    [SerializeField]
    private float maxHealth = 125; //max health

    [SerializeField]
    private float startHealth = 100; //max health

    [SerializeField]
    private float CurrentHealth;    //back office 
    public float currentHealth  //Current health we'll be modifying
    {
        get => CurrentHealth;
        set
        {
            CurrentHealth = value;  //set the back office variable
            if (CurrentHealth > maxHealth) CurrentHealth = maxHealth;
            HealthChanged?.Invoke(value - CurrentHealth);
            HealthModified?.Invoke(currentHealth, maxHealth); //we fire the Action
            if (value <= 0) StartCoroutine(Die());  //if no life, then die
        }
    }

    private void Start()
    {
        currentHealth = startHealth;    //we set current health to startHealth
    }

    /// <summary>
    /// Die function
    /// </summary>
    IEnumerator Die()
    {
        
        
        if (gameObject.name.Contains("Pusher")) //if player
        {
            GetComponent<SpriteRenderer>().enabled = false;
            foreach (Transform t in transform) Destroy(t.gameObject);
            yield return new WaitForSeconds(1.5f);  //wait a bit
            SceneManager.LoadScene("MainMenu"); //go main menu
            
        }
        else
        {
            Destroy(gameObject);    //We destroy the gameObject because we're dead
        }
            
    }
}
