using System;
using UnityEngine;

public class DialogueResponseEvents : MonoBehaviour
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private ResponseEvent[] events;

    public DialogueObject DialogueObject => dialogueObject;
    public ResponseEvent[] Events => events;

    public void OnValidate()
    {
        if (dialogueObject == null) return;
        if (dialogueObject.Responses == null) return;
        //if (events != null && events.Length == dialogueObject.Responses.Length) return;

        // initialize eventsArray with as many events as there are responses
        if (events == null)
        {
            events = new ResponseEvent[dialogueObject.Responses.Length];
        }
        // if it already exists, resize it
        else
        {
            Array.Resize(ref events, dialogueObject.Responses.Length);
        }

        // check every response of the dialogue object
        for (int i = 0; i < dialogueObject.Responses.Length; i++)
        {
            Response response = dialogueObject.Responses[i];

            // if event already exists, update its name to name of responseOption
            if (events[i] != null)
            {
                events[i].eventName = response.ResponseText;
                continue;
            }

            events[i] = new ResponseEvent() { eventName = response.ResponseText };
        }
    }
}
