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

    private bool isRemoved;
    

    void Start()
    {
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
        //apply force to target's after a certain pullDistance reached
        if (Vector3.Distance(transform.position, playerSurfacePoint.transform.position) < pullDistance  && !CheckIfAutoAndEnemy())
        {
            speedX = GameManager.instance.GetSpeed();
            rb.AddForce((playerPullPoint.transform.position - transform.position).normalized * speedX);
            
        }
        
        // remove object after a certain fall
        if (transform.position.y < -0.5f && !isRemoved)
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
        
        //players pull trigger removes the targets being kinematic
        if (other.CompareTag("PullTrigger"))
        {
            rb.isKinematic = false;
        }
        
        //if hole is triggered then target changes layers to fall into the blackhole
        if (other.CompareTag("HoleTrigger") &&  !CheckIfAutoAndEnemy())
        {
            gameObject.layer = 11;
        }
    }
    
    
    //check if there is a enemy while an animation being played.
    private bool CheckIfAutoAndEnemy()
    {
        return CompareTag("Enemy") && !GameManager.instance.GetIsInputOpen();
    }
    
    
    
    
}
