using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int attachedCount = 0;
    public GameObject door;
    public GameObject player;
    private Camera mainCam;
    private bool isInputOpen = true;
    

    private int table1TargetCount;
    private int table2TargetCount;
    private float targetSpeed;
    public float baseSpeed;
    public float transactionSpeed;
    

    private int count;

    private List<GameObject>_targets;

    public float pullDistance;

    private void Awake()
    {
        instance = this;
        targetSpeed = baseSpeed;
        mainCam = Camera.main;
        mainCam.transform.position = new Vector3(0, 26, -17.5f);
        table1TargetCount = GameObject.FindGameObjectsWithTag("Table1").Length;
        table2TargetCount = GameObject.FindGameObjectsWithTag("Table2").Length;
    }

    public void DecreaseTable1Count()
    {
        table1TargetCount--;
        if (table1TargetCount == 0)
        {
            isInputOpen = false;
            SwitchToTable2();
        }
    }
    
    public void DecreaseTable2Count()
    {
        table2TargetCount--;
        if (table2TargetCount == 0)
        {
            Time.timeScale = 0;
        }
    }

    public void GameOver()
    {
        ShakeCam();
    }

    public void ShakeCam()
    {
        mainCam.GetComponent<CameraShaker>().Shake();
    }

    private void SwitchToTable2()
    {
        StartCoroutine(MovePlayerAndDoor(1f));
    }

    IEnumerator MovePlayerAndDoor(float duration)
    {
        Vector3 startPos = player.transform.position;
        Vector3 doorStartPos = door.transform.position;
        Vector3 doorTargetPos = new Vector3(doorStartPos.x,0.83f,doorStartPos.z);
        Vector3 targetPos = new Vector3(0,startPos.y,startPos.z);
        
        var t = 0f;
        while(t < 1)
        {
            t += Time.deltaTime / duration;
            player.transform.position = Vector3.Lerp(startPos, targetPos, t);
            door.transform.position = Vector3.Lerp(doorStartPos, doorTargetPos, t);
            yield return null;
        }
        
        StartCoroutine(MoveToSecondTable(2f));
    }

    IEnumerator MoveToSecondTable(float duration)
    {
        targetSpeed = transactionSpeed;
        float playerZVal = 16.6f;
        Vector3 camStartPos = mainCam.transform.position;
        Vector3 camTargetPos = new Vector3(camStartPos.x,camStartPos.y,2.4f);
        Vector3 playerStartPos = player.transform.position;
        Vector3 playerTargetPos = new Vector3(playerStartPos.x,playerStartPos.y,playerZVal);
        
        var t = 0f;
        while(t < 1)
        {
            t += Time.deltaTime / duration;
            player.transform.position = Vector3.Lerp(playerStartPos, playerTargetPos, t);
            mainCam.transform.position = Vector3.Lerp(camStartPos, camTargetPos, t);
            yield return null;
        }

        player.GetComponent<Player>().AdjustBoundraies(26.5f, 16.2f);
        isInputOpen = true;
        targetSpeed = baseSpeed;
    }

    public bool GetIsInputOpen()
    {
        return isInputOpen;
    }

    public float GetSpeed()
    {
        return targetSpeed;
    }
}
