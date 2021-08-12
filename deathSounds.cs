using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathSounds : MonoBehaviour
{
    public AudioSource deathClip;

    [SerializeField]
    private AudioClip[] deathClips;

    private void Awake()
    {
        deathClip = GetComponent<AudioSource>();
        AudioClip clip = GetRandomClip();
        deathClip.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        return deathClips[UnityEngine.Random.Range(0, deathClips.Length)];
    }
}
