using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ChangeScene : MonoBehaviour
{

    public GameObject Canvas;
    public GameObject MainMusic;
    public Image blackImage;
    Animator animFade;
    Animator musicFade;

    public void Start()
    {
        animFade = Canvas.GetComponent<Animator>();
        musicFade = MainMusic.GetComponent<Animator>();
    }

    public void setScene(int sceneID)
    {
        StartCoroutine(Transition(sceneID));
    }

    IEnumerator Transition(int sceneID)
    {
        blackImage.gameObject.SetActive(true);
        animFade.SetBool("isFade", true);
        musicFade.SetBool("isMusicTransition", true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneID);
    }
}
