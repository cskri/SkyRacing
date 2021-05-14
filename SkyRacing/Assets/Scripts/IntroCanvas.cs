using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCanvas : MonoBehaviour
{
   public AudioClip FinishSound;
    private bool soundStarted = false;
    AudioSource source;
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.Stop();
        soundStarted=false;
    }
    void Update()
    {
        if(!soundStarted)
            {
                source.clip = FinishSound;
                source.volume = 0.3f;
                source.loop = true;
                source.Play();
                soundStarted = true;
            }
    }
}
