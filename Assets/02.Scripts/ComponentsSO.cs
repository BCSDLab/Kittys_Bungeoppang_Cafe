using UnityEngine;

[CreateAssetMenu(fileName = "NewComponent", menuName = "Cooking/Component")]
public class ComponentsSO : ScriptableObject
{
    public string componentName;
    public GameObject ComponentPrefab;
    public string type;
    public string outputName;
    public GameObject outputPrefab;
    public GameObject Bungeobbang;
}