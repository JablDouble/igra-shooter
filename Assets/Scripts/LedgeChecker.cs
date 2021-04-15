using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeChecker : MonoBehaviour
{

    public bool isGrabbingLedge;
    public PlayerController playerController;
    Ledge ledge = null;

    private void OnTriggerEnter(Collider other) {
        ledge = other.gameObject.GetComponent<Ledge>();
        if (ledge != null) {
            playerController.ClimbOnTheWall(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        ledge = other.gameObject.GetComponent<Ledge>();
        if (ledge != null) {
            playerController.ClimbOnTheWall(false);
        }
    }
}
