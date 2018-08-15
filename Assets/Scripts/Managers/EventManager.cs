using Complete;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{

    Text eventmessage;
    private string displayMessage;

    void Awake()
    {
        // Set up the reference.
        eventmessage = GetComponent<Text>();

        SetDisplayMessage("");
    }
    private void Start()
    {
        PlayerShooting.OnWeaponEventMessage+= SetDisplayMessage;
        Health.OnHealthEventMessage += SetDisplayMessage;
    }

    public void OnDisable()
    {
        PlayerShooting.OnWeaponEventMessage -= SetDisplayMessage;
        Health.OnHealthEventMessage -= SetDisplayMessage;
    }

    public void SetDisplayMessage(string message)
    {
        displayMessage = message;
    }

    void Update()
    {
        eventmessage.text = displayMessage;
    }
}
