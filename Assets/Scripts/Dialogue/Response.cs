
using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] private string responseText; // Name of Option
    [SerializeField] private DialogueObject dialogueObject; //Dialogue to follow

    public string ResponseText => responseText;
    public DialogueObject DialogueObject => dialogueObject;
}
