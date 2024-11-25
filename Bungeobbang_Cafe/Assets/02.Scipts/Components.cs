using UnityEngine;

public class Components : MonoBehaviour
{
    public ComponentsSO componentData;

    void Start()
    {
        if (componentData != null)
        {
            componentName = componentData.componentName;
            type = componentData.type;
            componentPrefab = componentData.ComponentPrefab;
            outputName = componentData.outputName;
            outputPrefab = componentData.outputPrefab;
        }
    }

    public string componentName;
    public GameObject componentPrefab;
    public string type;
    public string outputName;
    public GameObject outputPrefab;
}