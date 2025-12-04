using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneController : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.Instance.PlayBGM("MenuBgm");
    }
    public void OnTestLevelButtonClicked()
    {
        SceneManager.LoadScene("Level-test");
    }
}
