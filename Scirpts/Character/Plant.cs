using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField]  protected float curHP;

    protected int PlantId{ get;  set; }
    protected float MaxHP { get;  set; }

    public float CurrentHP
    {
        get { return curHP; }
        set { curHP = value; }
    }

    protected string PlantName { get;  set; }

    public string plantDescription { get;  set; }

    public int Level {  get;  set; }

    public int cost;

    // ‹…À
    public virtual void GetDamage(float value)
    {
        Debug.Log("Plnat GetDamage");
        CurrentHP -= value;
        if (CurrentHP <= 0) Dead();
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
