using UnityEngine;

public class Level : MonoBehaviour
{

    public Color tableColor;
    public Color doorColor;
    public Color enemyColor;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.doorMaterial.color = doorColor;
        GameManager.instance.tableMaterial.color = tableColor;
        GameManager.instance.enemyMaterial.color = enemyColor;
        GameManager.instance.holeMaterial.color = tableColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
