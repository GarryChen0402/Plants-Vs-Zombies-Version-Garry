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
        AttackDamange = 10;
        AttackCD = 2f;
        attackTimer = 0f;
        fxPlayTimePoint = 0.32f;
        animator = GetComponent<Animator>();
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
        go.GetComponent<LinerProjectileTemplate>().MoveDir = 1;
        AudioManager.Instance.PlayFxAtTime("PeashooterAttack", fxPlayTimePoint, 0.5f);
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
