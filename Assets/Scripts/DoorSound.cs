using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour
{

    Rigidbody rigidbody;
    protected AudioSource audioSorce;
    public AudioClip doorAudio;
    private bool isMove;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSorce = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidbody.velocity != new Vector3(0, 0, 0)) {
            if (!isMove) {
                audioSorce.PlayOneShot(doorAudio);
            }
            isMove = true;
        } else {
            isMove = false;
        }
    }
}
