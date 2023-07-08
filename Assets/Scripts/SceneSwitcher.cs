using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    private int sceneId;

    private SceneChangeInfo.Difficulty _tempValue;

    private void Start()
    {
        _tempValue = SceneChangeInfo.Information;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneId);
    }

    public void CancelOptions()
    {
        SceneChangeInfo.Information = _tempValue;
        LoadScene();
    }
    
    public void SetEasyDifficulty()
    {
        SceneChangeInfo.Information = SceneChangeInfo.Difficulty.Easy;
    }
    
    public void SetNormalDifficulty()
    {
        SceneChangeInfo.Information = SceneChangeInfo.Difficulty.Normal;
    }
    
    public void SetHardDifficulty()
    {
        SceneChangeInfo.Information = SceneChangeInfo.Difficulty.Hard;
    }
}
