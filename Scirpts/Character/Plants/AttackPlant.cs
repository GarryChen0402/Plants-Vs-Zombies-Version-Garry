using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlant : Plant
{
    public GameObject projectilePrefab;

    [SerializeField]
    protected float attackCD;
    protected float attackTimer;
    [SerializeField]
    protected bool canAttack;
    [SerializeField]
    protected float attackDamange;

    protected float AttackCD
    {
        get { return  attackCD; }
        set { attackCD = value; }
    }

    protected bool CanAttack
    {
        get { return canAttack; }
        set { canAttack = value; }
    }

    protected float AttackDamange
    {
        get { return attackDamange; }
        set { attackDamange = value; }
    }


    protected virtual void Attack()
    {
        Debug.Log("Attack");
    }




}
