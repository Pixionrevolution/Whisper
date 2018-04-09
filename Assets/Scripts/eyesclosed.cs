using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fove.Managed;
using UnityEngine.UI;


public class eyesclosed : MonoBehaviour
{

    public GameObject lefteye;
    public GameObject rightEye;
    public GameObject GazeEyesClosed;
    public AudioManager AudioM;


    public Shader Standard;
    public Shader Wireframe;

    public bool closed;
    public bool instance = false;


    private bool Radio = false;
    private bool Window = false;
    private bool Frame = false;


    // Liste d'indices
    public List<GameObject>Indices;
    // temps passé les yeux fermés
    public float closedTime = 0.0f;
    private float timeOnIndice = 0.0f;
    // enum d'état des yeux dans le fove
    private EFVR_Eye fove;

    float timeTemp;
    float timeTempOnIndice;
    AudioSource sound;


    public AudioClip FondEyesClosed;
    public AudioClip PianoCrescendo;
    public AudioClip RadioON;
    public AudioClip FrameON;
    public AudioClip WindowON;

    private float distanceBetweenSphereAndIndice;
    private float distanceEyeIndice;


    


    void Start()
    {
        //AudioM = GameObject.FindObjectOfType<AudioManager>();
        Indices.AddRange(GameObject.FindGameObjectsWithTag("Indice"));
        fove = FoveInterface.CheckEyesClosed();
        sound = GetComponent<AudioSource>();
        Standard = Shader.Find("Standard");
        Wireframe = Shader.Find("SuperSystems/Wireframe-Transparent-Culled");

        
    }


    
    void Update()
    {
        fove = FoveInterface.CheckEyesClosed();

        if (fove == EFVR_Eye.Neither)  // Si les yeux sont ouverts
        {


            

            sound.clip = PianoCrescendo;
            if (sound.isPlaying) // Si le son de chargement du pouvoir est actif
            {
                sound.Stop();
                // l'arrête
            }

            timeTemp = Time.time;    
            closed = false;

            foreach (GameObject INDICE in Indices) // Pour chaque indice de la liste d'indices, les mute
            {

               

                distanceEyeIndice = Vector3.Distance(INDICE.transform.position, lefteye.transform.position);
                INDICE.GetComponent<AudioSource>().mute = true;

                if (INDICE.name == "frame_FrontWall_Big" && Frame)
                {
                    INDICE.GetComponent<AudioSource>().clip = FrameON;
                    Frame = false; 
                    
                }
                if (INDICE.name == "Radio" && Radio)
                {
                    INDICE.GetComponent<AudioSource>().clip = RadioON;
                    Radio = false;
                }
                if (INDICE.name == "WindowBorder_Right" && Window)
                {
                    INDICE.GetComponent<AudioSource>().clip = WindowON;
                    Window = false;
                }


            }
   
        }
        
        if (fove == EFVR_Eye.Both) // Si les yeux sont fermés
        {    
            closed = true;
            instance = true;
            sound.PlayOneShot(sound.clip, 0.2f); // Jouer le son de chargement une fois


           if (closedTime> sound.clip.length) // Si l'utilisateur a les yeux fermés plus longtemps que la durée du son de chargement, arrêter le son
            {
                sound.Stop();
                //sound.clip = FondEyesClosed;
                
                
                

            }
            if (closedTime >= 2.0f) // Si l'utilisateur ferme les yeux plus de 2 secondes, activer les son des objets
            {
                //Pour chaque 
                //sound.loop = true;
                //sound.PlayOneShot(sound.clip, 0.2f);

                foreach (GameObject INDICE in Indices) 
                {
                    timeTempOnIndice = Time.time;
                    distanceBetweenSphereAndIndice = Vector3.Distance(GazeEyesClosed.transform.position, INDICE.transform.position);
                    
                    INDICE.GetComponent<AudioSource>().mute = false;
                  
                    

                    if (distanceBetweenSphereAndIndice < 1)
                    {

                        if (INDICE.GetComponent<AudioSource>().isPlaying == false)
                        {
                            
                            
                            INDICE.GetComponent<AudioSource>().PlayOneShot(INDICE.GetComponent<AudioSource>().clip, 1f);

                            timeOnIndice = Time.time - timeTemp;

                            if (INDICE.name == "Radio" && timeOnIndice>= 2.0f )
                            {
                                Radio = true;
                                INDICE.GetComponent<Renderer>().material.shader = Wireframe;
                            }
                            if (INDICE.name == "frame_FrontWall_Big" && timeOnIndice >= INDICE.GetComponent<AudioSource>().clip.length)
                            {
                                Frame = true;
                                INDICE.GetComponent<Renderer>().material.shader = Wireframe;
                            }
                            if (INDICE.name == "WindowBorder_Right" && timeOnIndice >= INDICE.GetComponent<AudioSource>().clip.length)
                            {
                                Window = true;
                                INDICE.GetComponent<Renderer>().material.shader = Wireframe;
                            }

                            if (fove== EFVR_Eye.Neither)
                            {
                                INDICE.GetComponent<AudioSource>().Stop();
                            }

                        }
                        
                       
                        INDICE.GetComponent<AudioSource>().volume += 0.3f * Time.deltaTime;
                        sound.volume -= 0.3f * Time.deltaTime;
                       
                    }

                    else if (distanceBetweenSphereAndIndice > 1 && INDICE.GetComponent<AudioSource>().volume > 0)
                    {
                        INDICE.GetComponent<AudioSource>().volume -= 0.9f * Time.deltaTime;
                        sound.volume += 0.3f* Time.deltaTime;
                        
                    }
                    else
                    {
                        INDICE.GetComponent<AudioSource>().Stop();
                    }
                }
            }
            closedTime = Time.time - timeTemp;   
        }
    }




}
