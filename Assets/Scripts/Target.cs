using System;
using System.Collections;
using ScriptInspector;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    private GameObject playerPullPoint;
    private GameObject playerSurfacePoint;
    private Rigidbody rb;
    private Vector3 attachedPos;

    private int color;

    private float pullDistance;
    private float speedX;
    private Vector3 startPos;

    private bool isRemoved = false;
    

    void Start()
    {
        startPos = transform.position;
        pullDistance = GameManager.instance.pullDistance;
        playerPullPoint = GameObject.Find("PlayerPullPoint");
        playerSurfacePoint = GameObject.Find("DepthMask");
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        if (Vector3.Distance(transform.position, playerSurfacePoint.transform.position) < pullDistance)
        {
            speedX = GameManager.instance.GetSpeed();
            rb.AddForce((playerPullPoint.transform.position - transform.position).normalized * speedX);
            
        }
        
        if (transform.position.y < -1f && !isRemoved)
        {
            gameObject.SetActive(false);
            if (gameObject.CompareTag("Enemy"))
            {
                GameManager.instance.GameOver();
            }
            if (CompareTag("Table1"))
            {
                GameManager.instance.DecreaseTable1Count();
            }else if (CompareTag("Table2"))
            {
                GameManager.instance.DecreaseTable2Count();
            }

            isRemoved = true;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HoleTrigger"))
        {
            gameObject.layer = 11;
        }
    }
    
    
    
    
}
