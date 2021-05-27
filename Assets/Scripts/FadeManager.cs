using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public GameObject MobileController;
    Animator animFade;


    // Start is called before the first frame update
    void Start()
    {
        animFade = MobileController.GetComponent<Animator>();
        animFade.SetBool("isFadeEnd", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
