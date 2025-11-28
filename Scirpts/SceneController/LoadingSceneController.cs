using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Loading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    // Control the loading bar animation
    public Image loadingBarGrassMask;
    private const float loadingBarGrassMaskMaxWidth = 321;
    private const float loadingBarGrassMaskMaxTime = 4f;
    private float loadingBarGrassMaskSpeed;
    private float currentLoadingBarGrassMaskWidth;

    public Image loadingBarGrassRoll;
    private const float loadingBarGrassRollStartPositionX = 205f;
    private const float loadingBarGrassRollEndPositionX = 505f;
    private const float loadingBarGrassRollPathLength = 300f;
    private float loadingBarGrassRollSpeed;
    private float currentLoadingBarGrassRollPositionX;

    private bool isLoadingBarComplete = false;

    public TMP_Text loadingButtonText;
    public Button loadingButton;

    private void Awake()
    {
        LoadingBarAwake();
        StartLoadingMenuBgm();
        DisableLoadingButton();
    }

    private void Start()
    {
        LoadingBarStart();
    }

    private void Update()
    {
        LoadingBarUpdate();
    }
    private void LoadingBarAwake()
    {
        loadingBarGrassMaskSpeed = loadingBarGrassMaskMaxWidth / loadingBarGrassMaskMaxTime;
        currentLoadingBarGrassMaskWidth = 0f;
        loadingBarGrassRollSpeed = loadingBarGrassRollPathLength / loadingBarGrassMaskMaxTime;
        currentLoadingBarGrassRollPositionX = loadingBarGrassRollStartPositionX;
        isLoadingBarComplete = false;
    }

    private void LoadingBarStart()
    {
        SetMaskWidth(0);
        SetRollPosX(currentLoadingBarGrassRollPositionX);
    }

    private void LoadingBarUpdate()
    {
        currentLoadingBarGrassMaskWidth = math.min(currentLoadingBarGrassMaskWidth + loadingBarGrassMaskSpeed * Time.deltaTime, loadingBarGrassMaskMaxWidth);
        currentLoadingBarGrassRollPositionX = math.min(currentLoadingBarGrassRollPositionX + loadingBarGrassRollSpeed * Time.deltaTime, loadingBarGrassRollEndPositionX);
        SetMaskWidth(currentLoadingBarGrassMaskWidth);
        SetRollPosX(currentLoadingBarGrassRollPositionX);
        if(!isLoadingBarComplete && currentLoadingBarGrassRollPositionX == loadingBarGrassRollEndPositionX && currentLoadingBarGrassMaskWidth == loadingBarGrassMaskMaxWidth)
        {
            isLoadingBarComplete = true;
            SetLoadingBarGrassRollInvisible();
            UpdateLoadingButton();
            EnableLoadingButton();
        }
    } 

    private void SetMaskWidth(float width)
    {
        loadingBarGrassMask.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    private void SetRollPosX(float x)
    {
        Vector3 position = loadingBarGrassRoll.transform.position;
        position.x = x;
        loadingBarGrassRoll.GetComponent<RectTransform>().SetPositionAndRotation(position, Quaternion.identity);
    }

    private void SetLoadingBarGrassRollInvisible()
    {
        loadingBarGrassRoll.enabled = false;
    }
    
    private void StartLoadingMenuBgm()
    {
        AudioManager.Instance.PlayBGM("LoadingMenuBgm");
    }

    private void UpdateLoadingButton()
    {
        loadingButtonText.text = "Start game!";
    }

    private void DisableLoadingButton()
    {
        loadingButton.enabled = false;
    }

    private void EnableLoadingButton()
    {
        loadingButton.enabled = true;
    }

    public void OnLoadingButtonClicked()
    {
        AudioManager.Instance.PlayFx("ButtonClicked");
        SceneManager.LoadScene("MenuScene");
        AudioManager.Instance.StopBGM();
    }

}
