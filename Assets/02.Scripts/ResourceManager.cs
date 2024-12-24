using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI fameText;
    
    private int coinValue = 0;
    private int fameValue = 0;

    [SerializeField] bool isTrue = false;

    void Update()
    {
        if (isTrue)
        {
            AddCoin(50);
            AddFame(10);
        }
    }

    void AddCoin(int coinAmount)
    {
        coinValue += coinAmount;
        UpdateCoinText();
    }

    void AddFame(int fameAmount)
    {
        fameValue += fameAmount;
        UpdateFameText();
    }

    void UpdateCoinText()
    {
        coinText.text = coinValue.ToString();
    }

    void UpdateFameText()
    {
        fameText.text = fameValue.ToString();
    }
}
