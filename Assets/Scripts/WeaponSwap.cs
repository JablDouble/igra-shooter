using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{

    public int currentWeaponIndex = 0;
    public GameObject player;
    public GameObject gunIcons;
    public FixedButton ChangeGunButton;

    void Start() {
        SelectWeapon();
        player.GetComponent<PlayerController>().ch_animator.SetInteger("curWeaponRating", 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeGun()
    {
        if (currentWeaponIndex >= transform.childCount - 1)
        {
            currentWeaponIndex = 0;
        }
        else
        {
            currentWeaponIndex++;
        }

        player.GetComponent<PlayerController>().ch_animator.SetInteger("curWeaponRating", currentWeaponIndex + 1);

        SelectWeapon();
    }

    void SelectWeapon() {


        int i = 0;
        foreach (Transform weapon in transform) {

            if (i == currentWeaponIndex) {
                weapon.gameObject.SetActive(true);
            } else {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }


        int j = 0;
        foreach (Transform icon in gunIcons.transform)
        {

            if (j == currentWeaponIndex)
            {
                icon.gameObject.SetActive(true);
            }
            else
            {
                icon.gameObject.SetActive(false);
            }

            j++;
        }
    }
}
