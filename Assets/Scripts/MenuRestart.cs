using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuRestart : MonoBehaviour
{

    public Button restartButton;
    // Start is called before the first frame update

    void Awake()
    {
        restartButton.onClick.AddListener(OnClickRestartButton);   
    }

    // Update is called once per frame

    void OnClickRestartButton()
    {
        gameObject.SetActive(false);
        GameManager.instance.RestartLevel(0.25f);
    }
    
}
