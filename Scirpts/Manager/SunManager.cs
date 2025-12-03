using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SunManager : MonoBehaviour
{
    [SerializeField] private int sunPoint;
    //[SerializeField] private Vector3 sunTargetPosition;

    //[SerializeField] private float sunCollectTime;
    //private float curSunCollectTime;


    //Unity component
    public TMP_Text sunPointText;
    public GameObject sunPointPrefab;

    [SerializeField] private bool canSunGenerate;
    [SerializeField] private float sunGenerateTime;
    private float curSunGenerateTime;
    private float sunGenerateHeight;
    [SerializeField] private float sunFallSpeed;


    public static SunManager Instance { get; private set; }
    

    public int SunPoint
    {
        get { return sunPoint; }
        set {
            if (value >= 0) sunPoint = value;
            else sunPoint = 0;
        }
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        sunGenerateTime = 5f;
        sunGenerateHeight = 6f;
        sunFallSpeed = 1f;
        canSunGenerate = true;
        //sunCollectTime = 5f;
        //curSunCollectTime = 0f;
    }

    private void Update()
    {
        UpdateSunPointText();

        if (canSunGenerate) SunGenerateUpdate();
    }

    private void UpdateSunPointText()
    {
        if (sunPointText == null)
        {
            Debug.LogError("The sunPointText pointer is null...");
            return;
        }
        sunPointText.text = sunPoint.ToString();
    }

    public void AddSunPoint(int value)
    {
        SunPoint += value;
    }

    public bool SubSunPoint(int value)
    {
        if (sunPoint < value)
        {
            Debug.Log("The sun point not enough..");
            return false;
        }
        sunPoint -= value;
        return true;
    }

    private void RandomGenerateSunPoint()
    {
        if(sunPointPrefab  == null)
        {
            Debug.Log("The sunPointPrefab is a null pointer... check your inspector of sunManager");
            return;
        }
        float x = Random.Range(-4f, 5.5f);
        GameObject go = GameObject.Instantiate(sunPointPrefab, new Vector3(x, 6, 0), Quaternion.identity);
        go.GetComponent<SunPoint>().CanFall = true;
        go.GetComponent<SunPoint>().FallHeight = Random.Range(2.5f, -4);
        go.GetComponent<SunPoint>().FallSpeed = sunFallSpeed;
    }

    private void SunGenerateUpdate()
    {
        curSunGenerateTime += Time.deltaTime;
        if(curSunGenerateTime >= sunGenerateTime)
        {
            curSunGenerateTime -= sunGenerateTime;
            RandomGenerateSunPoint();
        }
    }


}
