using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundHelper : MonoBehaviour
{
    public AudioClip punchWind;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Hit()
    {
        audioSource.clip = punchWind;
        audioSource.Play();
    }


}
