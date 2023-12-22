using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    private AudioSource audio;

    [SerializeField] private AudioClip clip;
    void Start()
    {
        DontDestroyOnLoad(this);

        audio = GetComponent<AudioSource>();
        audio.clip = clip;
        audio.loop = true;
        audio.Play();
    }
}