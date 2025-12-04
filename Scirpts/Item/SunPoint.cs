using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SunPoint : MonoBehaviour
{
    [SerializeField] private Vector3 moveTargetPosition;
    private Vector3 moveStartPosition;
    private const float moveTargetMaxTime = 1.25f;

    private const int sunPoint = 25;
    private float curMoveTargetTime;

    //private float targetDistance;
    private Vector3 startMoveSpeed;
    private bool isClicked = false;

    private bool canFall = false;
    private float fallHeight;
    private float fallSpeed;

    private bool isProducedByPlant;
    private float autoFallSpeed;
    private float autoFallHeight;
    private float autoFallGravity = 10;


    public bool CanFall
    {
        get { return canFall; }
        set { canFall = value; }
    }

    public float FallHeight
    {
        get { return fallHeight; }
        set { fallHeight = value; }
    }

    public float FallSpeed
    {
        get { return fallSpeed; }
        set { fallSpeed = value; }
    }

    public bool IsProducedByPlant
    {
        get { return isProducedByPlant; }
        set { isProducedByPlant = value; }
    }
    public float AutoFallSpeed
    {
        get { return autoFallSpeed; }
        set { autoFallSpeed = value; }
    }
    public float AutoFallHeight
    {
        get { return autoFallHeight; }
        set { autoFallHeight = value; }
    }



    private void Awake()
    {
        moveTargetPosition = new Vector3(-6f, 4.5f, 0);
        curMoveTargetTime = 0f;
        fallSpeed = 1f;
        fallHeight = -4f;
        isProducedByPlant = false;
        autoFallSpeed = 2.5f;
        autoFallHeight = transform.position.y - 0.8f;

        GetComponent<Transform>().localScale = Vector3.one * 1.2f;
    }

    private void Update()
    {
        if (isClicked)
        {
            MoveUpdate();
        }

        if (canFall)
        {
            FallUpdate();
        }

        if (isProducedByPlant)
        {
            AutoFallUpdate();
        }
    }

    private void OnMouseDown()
    {
        OnSunPointClicked();
    }

    private void MoveUpdate()
    {
        curMoveTargetTime += Time.deltaTime;
        if (curMoveTargetTime >= moveTargetMaxTime)
        {
            SunManager.Instance.AddSunPoint(sunPoint);
            SunManager.Instance.sunPoints.Remove(gameObject);
            Destroy(gameObject);
        }
        Vector3 moveStepVector = startMoveSpeed * curMoveTargetTime / 2 / moveTargetMaxTime * (2 * moveTargetMaxTime - curMoveTargetTime) + moveStartPosition - transform.position;
        transform.Translate(moveStepVector);
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = (moveTargetMaxTime - curMoveTargetTime) / moveTargetMaxTime;
        GetComponent<SpriteRenderer>().color = color;
    }

    public void OnSunPointClicked()
    {
        Debug.Log("Collect the sun");
        AudioManager.Instance?.PlayFx("SunPoint");
        InitMoveParams();
    }

    private void InitMoveParams()
    {
        if (isClicked) return;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        isClicked = true;
        moveStartPosition = transform.position;

        startMoveSpeed = moveTargetPosition - moveStartPosition;
        startMoveSpeed = 2 * startMoveSpeed / moveTargetMaxTime;
        curMoveTargetTime = 0f;
    }


    private void FallUpdate()
    {
        if (isClicked) return;//已经点击了（收集）则禁止下落
        Vector3 position = transform.position;
        position.y = math.max(fallHeight, position.y - fallSpeed * Time.deltaTime);
        //transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        GetComponent<Transform>().SetPositionAndRotation(position, Quaternion.identity);
    }

    private void AutoFallUpdate()
    {
        if (isClicked) return;
        Vector3 position = transform.position;
        position.y += Time.deltaTime * AutoFallSpeed;
        AutoFallSpeed -= Time.deltaTime * autoFallGravity;
        position.y = math.max(AutoFallHeight, position.y);
        GetComponent<Transform>().SetPositionAndRotation(position, Quaternion.identity);
    }
}
