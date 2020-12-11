using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PotionEffect : ScriptableObject, CanEffect
{
    public abstract void ApplyEffect(Health health);
}
