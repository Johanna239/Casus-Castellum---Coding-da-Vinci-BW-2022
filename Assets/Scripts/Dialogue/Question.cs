
using UnityEngine;

[System.Serializable]
public class Question
{
    [SerializeField] private string answer; // Name of Option
    [SerializeField] private DialogueObject dialogueObject; //Dialogue to follow

    public string Answer => answer;
    public DialogueObject DialogueObject => dialogueObject;
}
