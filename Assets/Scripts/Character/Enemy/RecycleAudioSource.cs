using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleAudioSource : MonoBehaviour
{
    private AudioSource audiosource;
    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!audiosource.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }
}
