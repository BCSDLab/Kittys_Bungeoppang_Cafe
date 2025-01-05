using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField] GameObject guidePanel;
    static bool isFirst = true;

    void Start()
    {
        if (isFirst)
        {
            guidePanel.SetActive(true);
            isFirst = false;
        }
    }
}
