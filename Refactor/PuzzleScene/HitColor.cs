using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpriteRenderer))]
public class HitColor : MonoBehaviour
{
    [SerializeField]
    private float duration = 0.2f;

    private SpriteRenderer sprite;
    private Health health;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        health.HealthChanged += TriggerColorHit;
    }

    private void OnDisable()
    {
        health.HealthChanged -= TriggerColorHit;
    }

    void TriggerColorHit(float currentHealth)
    {
        if(currentHealth > 0)
            StartCoroutine(ColorIDontKnow(Color.green));
        else
            StartCoroutine(ColorIDontKnow(Color.red));
    }

    IEnumerator ColorIDontKnow(Color color)
    {
        sprite.color = color;
        yield return new WaitForSeconds(duration);
        sprite.color = Color.white;
    }
}
