using UnityEngine;
using UnityEngine.UI;

public class MenuRestart : MonoBehaviour
{
    public Button restartButton;
    void Awake()
    {
        restartButton.onClick.AddListener(OnClickRestartButton);   
    }
    void OnClickRestartButton()
    {
        gameObject.SetActive(false);
        GameManager.instance.RestartLevel();
    }
}
