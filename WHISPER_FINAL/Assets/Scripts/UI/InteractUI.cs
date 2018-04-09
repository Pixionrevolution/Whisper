using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fove.Managed;
using UnityEngine.UI;

public class InteractUI : MonoBehaviour
{

    GameObject leftEye;
    GameObject rightEye;
    public bool touchedCollider = false;
    public GameObject colliderButton;


    private EFVR_Eye foveStatus;
    private  float distanceToTargetLeft;
    private float distanceToTargetRight;
    private int defaultDistance = 40;
    private bool waitToCollide = false;

     void Awake()
    {   // Initialize variables to false 
        touchedCollider = false;
        waitToCollide = false;
        StartCoroutine("waitTimeSphere");

        leftEye = GameObject.FindWithTag("gazeLeft");
        rightEye = GameObject.FindWithTag("gazeRight");

        
    }

    void Update()
    {
         // Condition to enable the collide func with  "gaze point"
         if(waitToCollide == true)
        {
            distanceToTargetLeft = Vector3.Distance(leftEye.transform.position, colliderButton.transform.position);
            distanceToTargetRight = Vector3.Distance(rightEye.transform.position, colliderButton.transform.position);

            if (distanceToTargetLeft <= defaultDistance || distanceToTargetRight <= defaultDistance)
            {
                touchedCollider = true;
            }
            else
            {
                touchedCollider = false;
            }
        }
    }
        
        //Wait to start the Collide
         IEnumerator waitTimeSphere()
        {
            yield return  new WaitForSeconds(2);
            waitToCollide = true;
            yield return null;
        }

    }

    


    

