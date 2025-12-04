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




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnMenuButtonClick()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
