using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    int sceneActuelleIndex;
    [SerializeField] AudioClip cinematicDebut;
    [SerializeField] AudioClip inGame;
    [SerializeField] AudioClip generic;
    private AudioSource audiosource;

    public static SoundManager instance;

    public bool firstEcoute;

    // Start is called before the first frame update
    void Awake()
    {

        instance = this;

        DontDestroyOnLoad(this.gameObject);

        audiosource = GetComponent<AudioSource>();

   
        
        
    }



}
