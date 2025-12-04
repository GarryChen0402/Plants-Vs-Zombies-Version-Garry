using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Plant : MonoBehaviour
{
    protected int PlantId{ get;  set; }
    protected float MaxHP { get;  set; }

    public float CurrentHP { get; set; }

    protected string PlantName { get;  set; }

    public string plantDescription { get;  set; }

    public int Level {  get;  set; }

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
