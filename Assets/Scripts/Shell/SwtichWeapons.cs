
using System.Collections;
using UnityEngine;

public class SwitchWeapons : MonoBehaviour {

    public GameObject weapon01;
    public GameObject weapon02;


    public void switchweapon()
    {

        Debug.Log("switchingweapon");
        if (weapon01.activeSelf)
        {
            weapon01.SetActive(false);
            weapon02.SetActive(true);
        }
        else
        {
            weapon01.SetActive(true);
            weapon02.SetActive(false);
        }


    }
}
