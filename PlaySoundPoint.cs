using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundPoint : MonoBehaviour
{
    public AudioSource audioSrc;
    public bool hasPlayed;
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("Player") && hasPlayed == false)
        {
            Debug.Log("SphereCollision");
            audioSrc.Play();
            hasPlayed = true;
        }
    }
}
