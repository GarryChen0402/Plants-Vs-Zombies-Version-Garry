using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peashooter : AttackPlant
{
    public Animator animator;
    //[SerializeField]
    private float fxPlayTimePoint;
    private void Awake()
    {
        PlantId = 1;
        AttackDamange = 10;
        AttackCD = 2f;
        attackTimer = 0f;
        fxPlayTimePoint = 0.32f;
        MaxHP = 100;
        CurrentHP = MaxHP;

        animator = GetComponent<Animator>();
        cost = 100;
        //animator.enabled = true;
        DisableAnimator();
    }

    private void Start()
    {
        EnableAnimator();
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
        if(attackTimer >= attackCD)
        {
            attackTimer -= attackCD;
            if (canAttack) Attack();
        }
    }

    protected override void Attack()
    {
        Vector3 position = transform.position;
        position.x += 0.45f;
        position.y -= 0.40f;
        GameObject go = GameObject.Instantiate(projectilePrefab, position, Quaternion.identity);
        go.GetComponent<PeaBullet>().MoveDir = 1;
        go.GetComponent<PeaBullet>().TargetTag = "Zombies";
        AudioManager.Instance?.PlayFxAtTime("PeashooterAttack", fxPlayTimePoint, 0.5f);
    }

    private void DisableAnimator()
    {
        animator.enabled = false;
    }

    private void EnableAnimator()
    {
        animator.enabled = true;
    }

}
