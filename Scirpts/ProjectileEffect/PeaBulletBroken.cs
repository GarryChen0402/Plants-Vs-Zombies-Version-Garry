using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBulletBroken : MonoBehaviour
{
    private float moveSpeed;
    private float livingTime;
    private float currentLivingTime;
    private int moveDir;
    
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    public float LivingTime
    {
        get { return livingTime; } 
        set { livingTime = value; }
    }

    public int MoveDir
    {
        get { return moveDir; }
        set { moveDir = value; }
    }

    private void Awake()
    {
        MoveSpeed = 2f;
        LivingTime = 0.5f;
        currentLivingTime = 0f;
    }

    

    private void Update()
    {
        currentLivingTime += Time.deltaTime;
        RendererAlphaUpdate();
        if (currentLivingTime >= livingTime)Destroy(gameObject);
        //transform.Translate()
        if(moveDir == 1)transform.Translate(Vector3.right * moveSpeed *  Time.deltaTime);
        else transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    private void RendererAlphaUpdate()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = (livingTime - currentLivingTime) / livingTime;
        GetComponent<SpriteRenderer>().color = color;
    }


}
