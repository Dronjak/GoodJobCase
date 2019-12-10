using System;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{ 
    private Vector3 startPos;
    private Vector3 playerPos;
    private bool isTouched;

    private Camera mainCam;

    private bool mouseClicked;

    private float upperBound = 7.10f;
    private float bottomBound = 3.45f;
    private float xBound = 3.30f;
    

    private void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    void Update()
    {
        CheckInput();
    }

    void Movement(){
        if (isTouched)
        {
            Vector3 distancePos = (mainCam.ScreenToViewportPoint(Input.mousePosition)*5 - startPos);
            var temp = distancePos.y;
            distancePos.y = 0;
            distancePos.z = temp * 4;
            distancePos.x *= 2;
            
            Vector3 targetPosition = playerPos + distancePos;

        
           if (targetPosition.x > xBound)
           {
               targetPosition.x = xBound;
           }

           if (targetPosition.x < -xBound)
           {
               targetPosition.x = -xBound;
           }
           
           if (targetPosition.z > upperBound)
           {
               targetPosition.z = upperBound;
           }

           if (targetPosition.z < -bottomBound)
           {
               targetPosition.z = -bottomBound;
           }

           
           var delta = 6*Time.deltaTime;
           delta *= Vector3.Distance(transform.localPosition, targetPosition);
           Vector3 movePos = Vector3.MoveTowards(transform.localPosition, targetPosition, delta);
           transform.position = movePos;
        }
    }

    void CheckInput()
    {
        if (GameManager.instance.GetIsInputOpen())
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos =  mainCam.ScreenToViewportPoint(Input.mousePosition)*5;
                playerPos = transform.localPosition;
                isTouched = true;
            }

            if (Input.GetMouseButtonUp(0) && isTouched)
            {
                isTouched = false;
            }
            
        }
        else
        {
            isTouched = false;
        }
    }

    public void AdjustBoundraies(float upperBound, float bottomBound)
    {
        this.upperBound = upperBound;
        this.bottomBound = bottomBound;
    }
    
}
