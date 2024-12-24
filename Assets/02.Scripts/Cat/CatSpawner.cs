using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject[] catPrefabs; // 고양이 프리팹 배열

    public void SpawnRandomCat(EventData eventData)
    {
        // 랜덤으로 고양이 선택
        int randomIndex = Random.Range(0, catPrefabs.Length);
        GameObject selectedCat = Instantiate(catPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);

        // 이벤트 전달
        MainCat catScript = selectedCat.GetComponent<MainCat>();
        if (catScript != null)
        {
            catScript.OnEventReceived.Invoke(eventData);
        }
    }
}