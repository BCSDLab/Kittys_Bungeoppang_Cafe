using System.Collections;
using UnityEngine;

public class MixerController : MonoBehaviour, ICookingTools
{
    [SerializeField] private float cookingTime = 5f;
    [SerializeField] MixerTimeBar mixerTimeBar;
    private ComponentsSO currentIngredient;
    


    // �ͼ��⿡ ��� �ֱ�
    public void CheckComponent(GameObject ingredient)
    {
        Components component = ingredient.GetComponent<Components>();

        if (currentIngredient != null)
        {
            Debug.Log("�̹� �ٸ� ��ᰡ �ȿ� ��� �ֽ��ϴ�.");
            return;
        }

        if (component != null && component.componentData != null)
        {
            currentIngredient = component.componentData;
            mixerTimeBar.StartTimer();
            StartCoroutine(ProcessCooking());
        }
    }

    // 5�� ���� ����
    public IEnumerator ProcessCooking()
    {
        Debug.Log("��Ḧ ���� ���Դϴ�...");
        yield return new WaitForSeconds(cookingTime);
        CreateOutput();
        currentIngredient = null;
    }

    // ��ٸ� �Ŀ� �ʸ��� �ֺ��� ����
    public void CreateOutput()
    {
        GameObject output = Instantiate(currentIngredient.outputPrefab);
        float random_x = Random.Range(0.8f, -0.8f);
        output.transform.position = this.transform.position + new Vector3(random_x, -0.8f, 0);
    }
}
