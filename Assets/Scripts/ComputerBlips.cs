using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ComputerBlips : MonoBehaviour
{
    private AudioSource source;
    public AudioClip computerBlip;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (UnityEngine.Random.Range(0f, 1f) > 0.95)
        {
            source.pitch = UnityEngine.Random.Range(1.3f, 1.8f);
            source.PlayOneShot(computerBlip);
        }
    }
}
