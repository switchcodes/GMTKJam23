using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    private int sceneId;

    public void LoadScene()
    {
        SceneChangeInfo.Information = SceneChangeInfo.Difficulty.Normal;
        SceneManager.LoadScene(sceneId);
    }
}
