using UnityEngine;
using UnityEngine.UI;

public class MenuNextLevel : MonoBehaviour
{
    public Button nextLevelButton;
    void Awake()
    {
        nextLevelButton.onClick.AddListener(OnClickNextLevelButton);   
    }
    void OnClickNextLevelButton()
    {
        gameObject.SetActive(false);
        GameManager.instance.StartNextLevel();
    }
}