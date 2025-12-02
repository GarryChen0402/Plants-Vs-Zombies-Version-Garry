using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int PlantId{ get; private set; }
    public float MaxHP { get; private set; }

    public float CurrentHP { get; set; }

    public string PlantName { get; private set; }

    public string plantDescription { get; private set; }

    public int Level {  get; private set; }

    // ‹…À
    protected virtual void GetDamage(float value)
    {
        CurrentHP -= value;
    }
    //÷Œ¡∆
    protected virtual void Heal(float value)
    {
        CurrentHP += value;
        CurrentHP = math.min(CurrentHP, MaxHP);
    }

    protected virtual void Dead()
    {
        Destroy(gameObject);
    }
}
