using System;
using System.Collections;
using ScriptInspector;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    private GameObject player;
    private GameObject playerCylinder;
    private Rigidbody rb;
    private Vector3 attachedPos;
    private bool isAttached = false;
    private bool isOnHub = false;
    private bool isThrowing = false;

    private int color;
    private bool isAttachable = true;
    private Collider collider;
    private bool isCollided;


    private float pullDistance;
    private bool isScalingDown;
    private float speedX = 10f;

    private void Awake()
    {
        //GameManager.instance.AddTarget(gameObject);
    }

    void Start()
    {
        pullDistance = GameManager.instance.pullDistance;
        player = GameObject.Find("Player");
        playerCylinder = GameObject.Find("PlayerCylinder");
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    void FixedUpdate()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        if (Vector3.Distance(transform.position, playerCylinder.transform.position) < pullDistance)
        {
            speedX = GameManager.instance.GetSpeed();
            rb.AddForce((player.transform.position - transform.position) * speedX);
            
        }
        
        if (transform.position.y < 0.5f)
        {
            //gameObject.SetActive(false);
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
        }
    }
    
    IEnumerator ScaleDown(float duration, Vector3 startScale)
    {
        if (isScalingDown)
        {
            yield break;
        }

        isScalingDown = true;
        var t = 0f;        
        Vector3 targetScale = transform.localScale * 0.8f;
        while(t < 1)
        {
            t += Time.deltaTime / duration;
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        isScalingDown = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //StartCoroutine(ScaleDown(0.1f,transform.localScale));
            collider.enabled = false;
        }
    }
    
}
