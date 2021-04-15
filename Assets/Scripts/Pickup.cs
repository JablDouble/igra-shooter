using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    PickupItem pickup;
    GameObject currentItem;
    private Animator ch_animator;

    // Start is called before the first frame update
    void Start()
    {
        ch_animator = this.transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E)) {
            PickUp();
        }
    }

    private void OnTriggerEnter(Collider other) {
        pickup = other.gameObject.GetComponent<PickupItem>();
        if (pickup != null) {
            currentItem = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        pickup = other.gameObject.GetComponent<PickupItem>();
        if (pickup != null) {
            currentItem = null;
        }
    }

    private void PickUp() {
        if (currentItem) {
            ch_animator.SetTrigger("PickUp");
            Destroy(currentItem);
        }
    }
}
