using UnityEngine;

public class Level : MonoBehaviour
{
    public Color tableColor;
    public Color doorColor;
    public Color enemyColor;
    void Start()
    {
        GameManager.instance.doorMaterial.color = doorColor;
        GameManager.instance.tableMaterial.color = tableColor;
        GameManager.instance.enemyMaterial.color = enemyColor;
        GameManager.instance.holeMaterial.color = tableColor;
    }
}
