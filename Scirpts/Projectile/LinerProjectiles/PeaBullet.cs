using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : LinerProjectileTemplate
{
    private void Awake()
    {
        MoveDir = 1;
        MoveSpeed = 2f;
        AttackDamage = 10f;
        RotationSpeed = 180f;
        projectileHeight = 0.65f;
    }

    protected override void OnBroken()
    {
        //Debug.Log("Peabullet on broken");
        AudioManager.Instance?.PlayFx("PeaBulletHit");
        Destroy(gameObject);
        GameObject go = GameObject.Instantiate(brokenEffectPrefab, projectile.transform.position, Quaternion.identity);
        go.GetComponent<PeaBulletBroken>().MoveDir = MoveDir;
        //Destroy(go, )
        //base.OnBroken();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Peabullet collision : " + collision.tag);
        if(collision.tag == targetTag)
        {
            if (collision.tag == "Zombies") collision.GetComponent<Zombie>().Hurt(AttackDamage);
            OnBroken();
        }
    }
}
