using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioiClip : MonoBehaviour
{   
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        Destroy(gameObject, _audioSource.clip.length);
    }
}
