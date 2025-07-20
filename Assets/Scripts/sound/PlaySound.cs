using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioClip clip;
    private AudioSource audioSource;
    float i = 1.0f;
    float imax = 2.2f;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            audioSource.Play();
            if (i < imax) i = i + 0.1f;
            else i = 1;
            audioSource.pitch = i;
        }
        
    }
}