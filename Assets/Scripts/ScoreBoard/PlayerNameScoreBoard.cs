using UnityEngine;
using UnityEngine.UI;

public class PlayerNameScoreBoard : MonoBehaviour {

    public static string displayname = "";       
    Text playername;                        

    void Awake()
    {
        // Set up the reference.
        playername = GetComponent<Text>();

        playername.text  = "";
    }


    void Update()
    {
        playername.text = displayname;
    }
}
