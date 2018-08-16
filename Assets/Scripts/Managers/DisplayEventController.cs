using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEventController : MonoBehaviour
{
    public int NoofMessagestoDisplay= 5;
    Text eventmessage;
    List<string> displayMessages = new List<string>();


    void Awake()
    {
        // Set up the reference.
        eventmessage = GetComponent<Text>();
       
    }
    private void Start()
    {
        EventManager.StartListening("Message", SetDisplayMessage);
    }
    public void OnEnable()
    {
        eventmessage.enabled = true;        
        SetDisplayMessage("");

    }

    public void OnDisable()
    {

        EventManager.StopListening("Message", SetDisplayMessage);
        eventmessage.enabled = false;
    }

    public void SetDisplayMessage(string message)
    {
        if(message == "GameOver") { OnDisable(); }
        displayMessages.Add(string.Format("{0}", message));
    }

    void Update()
    {
       if(displayMessages.Count > NoofMessagestoDisplay) { displayMessages.RemoveAt(0); }
       
        eventmessage.text = string.Join("\r\n", displayMessages.ToArray());

    }


}
