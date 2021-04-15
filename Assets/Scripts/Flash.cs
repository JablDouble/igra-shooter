using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{

    private bool isFlickering = false;
    private float timeDelay;
	public float timeDelayMin;
	public float timeDelayMax;

    // Update is called once per frame
    void Update()
    {
        if (isFlickering == false) {
			StartCoroutine(FlickeringLight());
		}
    }

    IEnumerator FlickeringLight() {
	isFlickering = true;
	this.gameObject.GetComponent<Light>().enabled = false;
	timeDelay = Random.Range(timeDelayMin, timeDelayMax);
	yield return new WaitForSeconds(timeDelay);
	this.gameObject.GetComponent<Light>().enabled = true;
	timeDelay = Random.Range(timeDelayMin, timeDelayMax);
	yield return new WaitForSeconds(timeDelay);
	isFlickering = false;
    }
}
