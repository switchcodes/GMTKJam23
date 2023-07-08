using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShowDifficulty : MonoBehaviour
{
    [SerializeField] private Button easyDifficulty;
    [SerializeField] private Button normalDifficulty;
    [SerializeField] private Button hardDifficulty;
    void Start()
    {
        switch (SceneChangeInfo.Information)
        {
            case SceneChangeInfo.Difficulty.Easy: easyDifficulty.Select(); break;
            case SceneChangeInfo.Difficulty.Normal: normalDifficulty.Select(); break;
            case SceneChangeInfo.Difficulty.Hard: hardDifficulty.Select(); break;
        }
    }
}
