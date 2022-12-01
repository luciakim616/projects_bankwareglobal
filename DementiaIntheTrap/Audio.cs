using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public float DelayTime;
    AudioSource myaudio;
    // Start is called before the first frame update
    void Start()
    {
        myaudio = GetComponent<AudioSource>();
        myaudio.PlayDelayed(DelayTime);
    }

}
