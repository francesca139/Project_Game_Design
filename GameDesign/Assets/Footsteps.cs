using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Footsteps : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClip;
    private AudioSource audioSource;    

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
    private void Movement()
    {
        AudioClip clip = GetRandomClip();
	audioSource.PlayOneShot(clip);
    }
    private AudioClip GetRandomClip()
    {
	int index = Random.Range(0,audioClip.Length - 1);
	return audioClip[index];
    }
}
