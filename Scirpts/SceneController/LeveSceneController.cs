using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeveSceneController : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.Instance.PlayBGM("GrassBgm");
    }
}
