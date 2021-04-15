using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Footstep : MonoBehaviour
{
    public AudioClip[] stepsAudio;
    protected AudioSource audioSorce;

    // Start is called before the first frame update
    void Start()
    {
        audioSorce = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Step()
    {
        AudioClip clip = GetRandomClip();
        audioSorce.volume = Random.Range(0.4f, 0.6f);
        audioSorce.pitch = Random.Range(0.8f, 1.5f);
        audioSorce.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip() {
        int index = Random.Range(0, stepsAudio.Length - 1);

        return stepsAudio[index];
    }
}
