using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI fameText;
    
    public int coinValue = 0;
    public int fameValue = 0;

    [SerializeField] bool isTrue = false;

    void Update()
    {
        if (isTrue)
        {
            AddCoin(50);
            AddFame(10);
        }
    }

    public void AddCoin(int coinAmount)
    {
        coinValue += coinAmount;
        UpdateCoinText();
    }

    public void AddFame(int fameAmount)
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
