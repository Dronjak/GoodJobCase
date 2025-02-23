﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject door;
    public Player player;
    public CameraShaker mainCam;
    private bool isInputOpen = true;
    private int table1TargetCount;
    private int table2TargetCount;
    private float targetSpeed;
    public float baseSpeed;
    public float transactionSpeed;
    public ParticleSystem confetti;
    public GameObject restartMenu;
    public GameObject nextLevelMenu;
    private static int currentLevel = 0;
    public List<GameObject> levels;
    public Material doorMaterial;
    public Material tableMaterial;
    public Material enemyMaterial;
    public Material holeMaterial;
    private int count;
    public float pullDistance;
    private void Awake()
    {
        instance = this;
        Instantiate(levels[currentLevel]);
        targetSpeed = baseSpeed;
        if (mainCam != null) mainCam.transform.position = new Vector3(0, 26, -17.5f);
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
            isInputOpen = false;
            confetti.Play();
            nextLevelMenu.SetActive(true);
        }
    }
    public void StartNextLevel()
    {
        if (currentLevel < levels.Count - 1)
        {
            currentLevel++;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isInputOpen = true;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isInputOpen = true;
    }
    public void GameOver()
    {
        isInputOpen = false;
        ShakeCam();
        restartMenu.SetActive(true);
    }
    public void ShakeCam()
    {
        mainCam.Shake();
    }
    private void SwitchToTable2()
    {
        StartCoroutine(MovePlayerAndDoor(1));
    }
    IEnumerator MovePlayerAndDoor(float duration)
    {
        Vector3 startPos = player.transform.position;
        Vector3 doorStartPos = door.transform.position;
        Vector3 doorTargetPos = new Vector3(doorStartPos.x,0.83f,doorStartPos.z);
        Vector3 targetPos = new Vector3(0,startPos.y,startPos.z);

        float playerDistance = Vector3.Distance(startPos, targetPos);
        float doorDistance = Vector3.Distance(doorStartPos, doorTargetPos);

        float startTime = Time.time;
        while(startTime + duration > Time.time)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPos, Time.deltaTime * playerDistance / duration);
            door.transform.position = Vector3.MoveTowards(door.transform.position, doorTargetPos, Time.deltaTime * doorDistance / duration);
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
        float playerTotalDistance = Vector3.Distance(playerTargetPos, playerStartPos);
        float cameraDistance = Vector3.Distance(camTargetPos, camStartPos);
        
        float startTime = Time.time;
        while(startTime + duration > Time.time)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, playerTargetPos, Time.deltaTime * playerTotalDistance / duration);
            mainCam.transform.position = Vector3.MoveTowards(mainCam.transform.position, camTargetPos, Time.deltaTime * cameraDistance / duration);
            yield return null;
        }

        player.AdjustBoundaries(26.5f, -16.5f);
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
