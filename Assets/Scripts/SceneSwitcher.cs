using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    private int sceneId;

    private SceneChangeInfo.DifficultyEnum _tempValue;

    private void Start()
    {
        _tempValue = SceneChangeInfo.Difficulty;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneId);
    }

    public void CancelOptions()
    {
        SceneChangeInfo.Difficulty = _tempValue;
        LoadScene();
    }
    
    public void SetEasyDifficulty()
    {
        SceneChangeInfo.Difficulty = SceneChangeInfo.DifficultyEnum.Easy;
    }
    
    public void SetNormalDifficulty()
    {
        SceneChangeInfo.Difficulty = SceneChangeInfo.DifficultyEnum.Normal;
    }
    
    public void SetHardDifficulty()
    {
        SceneChangeInfo.Difficulty = SceneChangeInfo.DifficultyEnum.Hard;
    }
}
