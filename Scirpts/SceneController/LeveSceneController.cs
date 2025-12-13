using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum GameState
{
    Loading,
    RightMove,
    LeftMove,
    PreReady,
    Ready,
    Gaming,
    Win, 
    Lose
}

public class LeveSceneController : MonoBehaviour
{
    public static LeveSceneController Instance { get; private set; }

    private float rightTarget;
    private float leftTarget;
    private float moveTime;
    private float moveTimer;

    private GameState state;

    public Image LoseUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        AudioManager.Instance?.PlayBGM("GrassBgm");
        state = GameState.Loading;
        rightTarget = 5.5f;
        leftTarget = -1.5f;
        moveTime = 2;
        moveTimer = 0;
        LoseUI.gameObject.GetComponent<Animator>().enabled = false;
    }

    private void Start()
    {
        SwitchToRightMoveState();
    }

    public void OnMenuButtonClick()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.Loading:
                break;
            case GameState.RightMove:
                RightMoveUpdate();
                break;
            case GameState.LeftMove:
                LeftMoveUpdate();
                break;
            case GameState.PreReady:
                PreReadyUpdate();
                break;
            case GameState.Ready:
                ReadyUpdate();
                break;
            case GameState.Gaming:
                PlantsAndZombiesUpdate();
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                LoseUpdate();
                break;
        }
        //PlantsAndZombiesUpdate();
    }

    private void PlantsAndZombiesUpdate()
    {
        for(int i = 0;i < ZombieManager.Instance.livingZombies.Count; i++)
        {
            if (ZombieManager.Instance.livingZombies[i].Count == 0) PlantsManager.Instance.DisableAttack(i);
            else PlantsManager.Instance.EnableAttack(i);
        }
        if (ZombieManager.Instance.allZombieGenerated && ZombieManager.Instance.allZombieCleared) SwitchToGameWinState();
    }

    public void GameLose()
    {
        state = GameState.Lose;
        AudioManager.Instance?.StopBGM();
        AudioManager.Instance?.PlayFx("GameLose");
        LoseUI.gameObject.GetComponent<Animator>().enabled = true;
        SwitchToGameEndState();
    }

    private void SwitchToGameEndState()
    {
        SunManager.Instance.CanProduce = false;
        PlantsManager.Instance.DisableAllPlants();
        ZombieManager.Instance.DisableZombies();
    }

    private void SwitchToRightMoveState()
    {
        state = GameState.RightMove;
    }
    
    private void SwitchToLeftMoveState()
    {
        state = GameState.LeftMove;
        moveTimer = 0;
    }

    private void SwitchToPreReady()
    {
        state = GameState.PreReady;
        moveTimer = 0;
    }

    private void SwitchToReady()
    {
        state = GameState.Ready;
    }

    private void SwitchToGaming()
    {
        state = GameState.Gaming;
        SunManager.Instance.CanProduce = true;

    }

    private void SwitchToGameWinState()
    {
        state = GameState.Win;
        AudioManager.Instance?.StopBGM();
        AudioManager.Instance?.PlayFx("GameWin");
        SwitchToGameEndState();
    }

    private void RightMoveUpdate()
    {
        //moveTimer += Time.deltaTime;
        if(moveTimer >= moveTime)
        {
            SwitchToLeftMoveState();
            return;
        }
        float currentMoveTime;
        if (moveTimer + Time.deltaTime >= moveTime) currentMoveTime = moveTime - moveTimer;
        else currentMoveTime = Time.deltaTime;
        moveTimer += Time.deltaTime;
        Camera.main.transform.Translate(Vector3.right * currentMoveTime * rightTarget / moveTime);

    }

    private void LeftMoveUpdate()
    {
        //moveTimer += Time.deltaTime;
        if (moveTimer >= moveTime)
        {
            SwitchToPreReady();
            return;
        }
        float currentMoveTime;
        if (moveTimer + Time.deltaTime >= moveTime) currentMoveTime = moveTime - moveTimer;
        else currentMoveTime = Time.deltaTime;
        moveTimer += Time.deltaTime;
        Camera.main.transform.Translate(Vector3.left * currentMoveTime * (rightTarget - leftTarget) / moveTime);

    }

    private void PreReadyUpdate()
    {
        //moveTimer += Time.deltaTime;
        if (moveTimer >= moveTime)
        {
            SwitchToReady();
            return;
        }
        float currentMoveTime;
        if (moveTimer + Time.deltaTime >= moveTime) currentMoveTime = moveTime - moveTimer;
        else currentMoveTime = Time.deltaTime;
        moveTimer += Time.deltaTime;
        Camera.main.transform.Translate(Vector3.right * currentMoveTime * (0 - leftTarget) / moveTime);

    }

    private void ReadyUpdate()
    {
        SwitchToGaming();
    }

    private void LoseUpdate()
    {
        
    }
}
