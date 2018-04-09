using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fove.Managed;

public class audioScript : MonoBehaviour
{

    public GameObject target;
    private AudioSource audioClip;
    //public GameObject fove;
    private EFVR_Eye foveStatus;
    public GameObject fove;
    



    public float distanceToTarget;
    


    void Start()
    {

        
    
        audioClip = GetComponent<AudioSource>();
       // audioClip.volume = 0;
        



        

    }
    void Update()
    {


        if (foveStatus == EFVR_Eye.Neither)

        {
            //    GetComponent<AudioSource>().mute = true;
                Debug.Log("pas de son");
            GetComponent<AudioSource>().mute = false;
            if (distanceToTarget <= 1 && fove.GetComponent<eyesclosed>().closedTime >= 2.0f)
            {

                // GetComponentInChildren<Canvas>().enabled = true;



            }
        }

        distanceToTarget = Vector3.Distance(target.transform.position, this.transform.position);


        if (foveStatus == EFVR_Eye.Both)
        {
           
            Debug.Log("AUdiosource opé");
           

        }

        if (distanceToTarget < 5 && foveStatus == EFVR_Eye.Both)
        {
            Debug.Log("Je joue le son");

            audioClip.volume = 1 / distanceToTarget;
            //Debug.Log("Le volume est à " + audioClip.volume);

        }




        //Debug.Log(distanceToTarget);
        //Debug.Log(target.transform.position);







        else
        {
           audioClip.volume = 0f;
        }
             }
            //audioClip.volume = 1 * distanceToTarget;
        





    }

    


    

