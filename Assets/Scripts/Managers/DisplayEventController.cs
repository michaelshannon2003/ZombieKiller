using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEventController : MonoBehaviour
{

    Text eventmessage;
    List<string> displayMessages =  new List<string>();
    

    void Awake()
    {
        // Set up the reference.
        eventmessage = GetComponent<Text>();

        SetDisplayMessage("");
    }
    private void Start()
    {
        EventManager.StartListening("Message", SetDisplayMessage);
    }

    public void OnDisable()
    {
        EventManager.StopListening("Message", SetDisplayMessage);
    }

    public void SetDisplayMessage(string message)
    {
        displayMessages.Add(string.Format("{0}\r\n", message));

    }

    void Update()
    {
         displayMessages.Reverse();
        eventmessage.text =
        string.Join("\r\n", displayMessages.ToArray());
        displayMessages.Reverse();
    }
}
