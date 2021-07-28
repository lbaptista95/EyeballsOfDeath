using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioHandler : MonoBehaviour
{
    [SerializeField] private AudioClip walkClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip deathClip;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        GameManager.OnPlayerDeath += GameManager_OnPlayerDeath;
    }

    private void OnDisable()
    {
        GameManager.OnPlayerDeath -= GameManager_OnPlayerDeath;
    }

    private void GameManager_OnPlayerDeath()
    {
        source.pitch = 1;
        source.PlayOneShot(deathClip);
    }

    public void PlayWalkClip()
    {
        source.pitch = Random.Range(-0.5f, 1.5f);
        source.PlayOneShot(walkClip);
    }

    public void PlayJumpClip()
    {
        source.pitch = 1;
        source.PlayOneShot(jumpClip);
    }

}
