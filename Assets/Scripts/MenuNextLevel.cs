using UnityEngine;
using UnityEngine.UI;

public class MenuNextLevel : MonoBehaviour
{

    public Button nextLevelButton;
    // Start is called before the first frame update

    void Awake()
    {
        nextLevelButton.onClick.AddListener(OnClickNextLevelButton);   
    }

    // Update is called once per frame

    void OnClickNextLevelButton()
    {
        gameObject.SetActive(false);
        GameManager.instance.StartNextLevel();
    }
    
}