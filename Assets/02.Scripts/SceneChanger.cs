using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeToIntroScene()
    {
        SceneManager.LoadScene("Intro_Scene");
    }

    public void ChangeToGamePlayScene()
    {
        SceneManager.LoadScene("Play_Scene");
    }

    public void ChangeToBestEndingScene()
    {
        SceneManager.LoadScene("Ending_Best_Scene");
    }

    
    public void ChangeToNormalEndingScene()
    {
        SceneManager.LoadScene("Ending_Normal_Scene");
    }

    public void ChangeToBadEndingScene()
    {
        SceneManager.LoadScene("Ending_Bad_Scene");
    }
}
