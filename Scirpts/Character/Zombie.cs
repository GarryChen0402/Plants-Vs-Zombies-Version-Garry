using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    protected float maxHP;
    [SerializeField] protected float curHP;
    protected float attackDamage;
    protected string targetTag;

    protected bool canMove;
    public bool CanMove
    {
        get => canMove;
        set => canMove = value;
    }

    protected float attackCD;
    protected float attackTimer;

    public int ZombieId;

    public float AttackDamage => attackDamage;


    public virtual void Hurt(float value)
    {
        Debug.Log("The function : Hurt in Zombie, it should not be called, please override it in subclass");
    }

    protected void EnterTheHouse()
    {
        LeveSceneController.Instance.GameLose();
    }
}
