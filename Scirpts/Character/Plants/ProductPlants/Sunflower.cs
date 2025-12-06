using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Sunflower : ProductPlant
{
    private Animator animator;
    private void Awake()
    {
        productCD = 5f;
        productTimer = 0f;
        canProduce = true;
        animator = GetComponent<Animator>();
        MaxHP = 100;
        curHP = MaxHP;
    }

    protected override void ProductResourceUpdate()
    {
        //base.ProductResourceUpdate();
        productTimer += Time.deltaTime;
        if(productTimer >= productCD)
        {
            productTimer -= productCD;
            animator.SetTrigger("StartGenerate");
            //GenerateSunPoint();
        }
    }

    private void GenerateSunPoint()
    {
        Vector3 position = transform.position;
        float x_dis = Random.Range(0.5f, 0.8f);
        if (Random.Range(0, 1f) <= 0.5) x_dis *= -1;
        position.x += x_dis;
        position.y += 0.5f;
        GameObject go = GameObject.Instantiate(productResourcePrefab, position, Quaternion.identity);
        //go.GetComponent<SunPoint>().AutoFallHeight += 0.25f;
        go.GetComponent<SunPoint>().IsProducedByPlant = true;
        SunManager.Instance.sunPoints.Add(go);
        //AudioManager.Instance?.PlayFx("");
    }
}
