using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeveSceneController : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.Instance?.PlayBGM("GrassBgm");
    }
    public void OnMenuButtonClick()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void Update()
    {
        PlantsAndZombiesUpdate();
    }

    private void PlantsAndZombiesUpdate()
    {
        for(int i = 0;i < ZombieManager.Instance.livingZombies.Count; i++)
        {
            if (ZombieManager.Instance.livingZombies[i].Count == 0) PlantsManager.Instance.DisableAttack(i);
            else PlantsManager.Instance.EnableAttack(i);
        }
    }
}
