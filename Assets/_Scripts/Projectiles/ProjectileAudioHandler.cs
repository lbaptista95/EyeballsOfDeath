using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAudioHandler : MonoBehaviour
{
    [SerializeField] private AudioClip explosionClip;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();        
    }

    public void PlayExplosionClip()
    {
        source.PlayOneShot(explosionClip);
    }
}
