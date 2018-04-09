using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndiceManager : MonoBehaviour {

    public Animator windowAnim;

    private GameObject leftEye;
    private GameObject rightEye;
    private GameObject GazeEyesClosed;
    public AudioManager AudioM;


    public Shader Standard;
    public Shader Wireframe;

    public bool closed;
    public bool instance = false;


    private bool Radio = false;
    private bool Window = false;
    private bool Frame = false;

    public int indicesToComplete;
    private int indicesDone = 0;


    // Liste d'indices
    public List<GameObject> Indices;
    // temps passé les yeux fermés
    public float closedTime = 0.0f;
    private float timeOnIndice = 0.0f;


    float timeTemp;
    float timeTempOnIndice;
    AudioSource sound;

    private bool levelComplete = false;
    private bool levelNext = false;
    private bool endlevel = false;


    // public AudioClip FondEyesClosed;
    public AudioClip PianoCrescendo;
    public AudioClip RadioON;
    public AudioClip FrameON;
    public AudioClip WindowON;

    private float distanceBetweenSphereAndIndice;

    GameObject fove;

    void Awake()
    {
        leftEye = GameObject.FindGameObjectWithTag("leftEye");
        rightEye = GameObject.FindGameObjectWithTag("rightEye");
        GazeEyesClosed = GameObject.FindGameObjectWithTag("GazeEyesClose");
    }

    // Use this for initialization
    void Start () {
        fove = GameObject.Find("[FOVE]");

        Indices.AddRange(GameObject.FindGameObjectsWithTag("Indice"));
        sound = GetComponent<AudioSource>();
        Standard = Shader.Find("Standard");
        Wireframe = Shader.Find("SuperSystems/Wireframe-Transparent-Culled");
    }

    // Update is called once per frame
    void Update()
    {


        if (!levelComplete)
        {
            if (fove.GetComponent<EyesClosed>().isOpen)  // Si les yeux sont ouverts
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
                    INDICE.GetComponent<AudioSource>().mute = true;

                    if (INDICE.name == "frame_FrontWall_Big" && Frame)
                    {
                        indicesDone++;
                        INDICE.GetComponent<AudioSource>().clip = FrameON;
                        Frame = false;
                    }
                    if (INDICE.name == "Radio" && Radio)
                    {
                        indicesDone++;
                        INDICE.GetComponent<AudioSource>().clip = RadioON;
                        Radio = false;
                    }
                    if (INDICE.name == "WindowBorder_Right" && Window)
                    {
                        indicesDone++;
                        windowAnim.Play("Win_R_open");
                        INDICE.GetComponent<AudioSource>().clip = WindowON;
                        Window = false;
                    }
                }
            }

            if (!fove.GetComponent<EyesClosed>().isOpen) // Si les yeux sont fermés
            {
                closed = true;
                instance = true;
                sound.PlayOneShot(sound.clip, 0.2f); // Jouer le son de chargement une fois


                if (closedTime > sound.clip.length) // Si l'utilisateur a les yeux fermés plus longtemps que la durée du son de chargement, arrêter le son
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

                                if (INDICE.name == "Radio" && timeOnIndice >= 2.0f)
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
                            }

                            INDICE.GetComponent<AudioSource>().volume += 0.3f * Time.deltaTime;
                            sound.volume -= 0.3f * Time.deltaTime;
                        }

                        else if (distanceBetweenSphereAndIndice > 1 && INDICE.GetComponent<AudioSource>().volume > 0)
                        {
                            INDICE.GetComponent<AudioSource>().volume -= 0.9f * Time.deltaTime;
                            sound.volume += 0.3f * Time.deltaTime;
                        }
                        else
                        {
                            INDICE.GetComponent<AudioSource>().Stop();
                        }
                    }
                }
                closedTime = Time.time - timeTemp;
            }

        } else // Level is complete, wait for user to close his eye to go next level
        {
            if (!fove.GetComponent<EyesClosed>().isOpen && !levelNext && endlevel)
            {
                levelNext = true;
                GameObject.FindWithTag("UI_levelcomplete").GetComponent<SpriteRenderer>().enabled = false;
                GameObject.Find("[MANAGER]").GetComponent<LevelManager>().next();
            }
        }

        if (indicesDone == indicesToComplete && !endlevel)
        {
            endlevel = true;
            StartCoroutine("goNext");
        }
    }

    IEnumerator goNext()
    { 
        yield return new WaitForSeconds(4);
        levelComplete = true;
        GameObject.FindWithTag("UI_levelcomplete").GetComponent<SpriteRenderer>().enabled = true;
    }
}

