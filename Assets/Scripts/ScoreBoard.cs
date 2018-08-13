using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    public static string weapon = "";       


    Text weaponselected;                     


    void Awake()
    {
        // Set up the reference.
        weaponselected = GetComponent<Text>();
        
        weaponselected.text  = "";
    }


    void Update()
    {       
        weaponselected.text = weapon;
    }
}
