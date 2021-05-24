using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorJacking : MonoBehaviour
{

    public Canvas mobileController;
    public Canvas doorJacking;
    public int howManyHits;

    public AudioClip hitAudio, brokenAudio;
    protected AudioSource AudioSorcePlayer;

    private int hitCount = 0;
    private bool readyToHit = true;

    // Update is called once per frame
    void Update()
    {
        AudioSorcePlayer = GetComponent<AudioSource>();
    }

    public void hitTheLock()
    {
        if (readyToHit)
        {
            hitCount++;
            if (hitCount == howManyHits)
            {
                Rigidbody rbOfDoor = gameObject.GetComponent<Rigidbody>();
                rbOfDoor.isKinematic = false;
                closeJacking();
                AudioSorcePlayer.PlayOneShot(brokenAudio);
            }
            else
            {
                AudioSorcePlayer.PlayOneShot(hitAudio);
            }
            readyToHit = false;
            Invoke("reloadHitting", 0.7f);
        }
    }

    private void reloadHitting()
    {
        readyToHit = true;
    }


    public void openJacking()
    {
        mobileController.gameObject.SetActive(false);
        doorJacking.gameObject.SetActive(true);

    }

    public void closeJacking()
    {
       mobileController.gameObject.SetActive(true);
       doorJacking.gameObject.SetActive(false);
    }
}
