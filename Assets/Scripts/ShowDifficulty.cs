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
        switch (SceneChangeInfo.Difficulty)
        {
            case SceneChangeInfo.DifficultyEnum.Easy: easyDifficulty.Select(); break;
            case SceneChangeInfo.DifficultyEnum.Normal: normalDifficulty.Select(); break;
            case SceneChangeInfo.DifficultyEnum.Hard: hardDifficulty.Select(); break;
        }
    }
}
