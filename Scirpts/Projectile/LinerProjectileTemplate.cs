using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class LinerProjectileTemplate : MonoBehaviour
{
    // 抛射物 物体
    public GameObject projectile;
    // 抛射物 阴影
    public GameObject projectileShadow;

    public CircleCollider2D projectileCircleCollider2D;

    public GameObject brokenEffectPrefab;

    //Speed
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected float rotationSpeed;
    [SerializeField]
    protected int moveDir;
    [SerializeField]
    protected float projectileHeight;
    protected float attackDamage;

    protected string targetTag;
    public float MoveSpeed {
        get
        {
            return moveSpeed;
        }
        set {
            moveSpeed = value;
        }
    }
    public float RotationSpeed {
        get
        {
            return rotationSpeed;
        }
        set
        {
            rotationSpeed = value;
        }
    }
    public int MoveDir
    {
        get
        {
            return moveDir;
        }
        set
        {
            if (value > 0) moveDir = 1;
            else if (value < 0) moveDir = -1;
            else moveDir = 0;
        }
    }

    public float AttackDamage
    {
        get { return attackDamage; }
        set { attackDamage = value; }
    }
    public string TargetTag
    {
        get { return targetTag; }
        set { targetTag = value; }
    }

    private void Awake()
    {
        //projectileCircleCollider2D = GetComponent<CircleCollider2D>();
        InitProjectile();
        InitProjectileCollider();
        //InitProjectileHeight();
        //SetProjectileHeight();
    }

    private void Update()
    {
        MoveUpdate();
        RotateUpdate();
        //ProjectileHeightUpdate();
    }

    protected void InitProjectile()
    {
        projectile.GetComponent<Transform>().SetLocalPositionAndRotation(new Vector3(0, 1, 0), Quaternion.identity);
        //projectileHeight = 1f;
        SetProjectileHeight(projectileHeight);
    }

    protected void InitProjectileCollider()
    {
        projectileCircleCollider2D = GetComponent<CircleCollider2D>();
        projectileCircleCollider2D.radius = projectile.GetComponent<SpriteRenderer>().size.x;
        projectileCircleCollider2D.offset = projectile.transform.localPosition;
        //Debug.Log(projectile.GetComponent<SpriteRenderer>().size);
    }

    protected void MoveUpdate()
    {
        if(moveDir == 1)transform.Translate(Vector3.right *  moveSpeed * Time.deltaTime);
        else if(moveDir == -1)transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    protected void RotateUpdate()
    {
        projectile.transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
    }

    protected void ProjectileHeightUpdate()
    {
        SetProjectileHeight(projectileHeight);
    }



    protected void SetProjectileHeight(float value = 1.0f)
    {
        Vector3 position = projectile.transform.localPosition;
        //Quaternion rotation = Quaternion.identity;
        position.y = value;
        //position.x = 0;
        projectile.GetComponent<Transform>().SetLocalPositionAndRotation(position, Quaternion.identity);
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != targetTag) return;
        OnBroken();
        moveDir = 0;
        Debug.Log("Collision has enter.");
    }

    protected virtual void OnBroken()
    {
        Destroy(gameObject);
    }
    //{
    //    Destroy(gameObject);
    //    //GameObject go = GameObject.Instantiate(brokenEffectPrefab, projectile.transform.position, Quaternion.identity);
    //    //go.GetComponent<PeaBulletBroken>().MoveDir = moveDir;
    //    //Destroy(go, 1);
    //}

}
