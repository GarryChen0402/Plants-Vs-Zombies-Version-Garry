using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int PlantId{ get; private set; }
    public int MaxHP { get; private set; }

    private int CurrentHP { get; set; }

    public string PlantName { get; private set; }

    public string plantDescription { get; private set; }

    public int Level {  get; private set; }

    // ‹…À
    public virtual void Attack(int value)
    {
        CurrentHP -= value;
    }
    //÷Œ¡∆
    public virtual void Heal(int value)
    {
        CurrentHP += value;
        CurrentHP = math.min(CurrentHP, MaxHP);
    }
    
    public virtual void Dead()
    {
        Destroy(gameObject);
    }
}
