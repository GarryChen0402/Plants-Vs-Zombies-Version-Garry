using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneController : MonoBehaviour
{
    public void OnTestLevelButtonClicked()
    {
        SceneManager.LoadScene("Level-test");
    }
}
