using UnityEngine;

public class Exit : MonoBehaviour
{
    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
