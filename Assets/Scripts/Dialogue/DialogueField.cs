
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueField
{
    [SerializeField] private string speakingCharacter; //Dialogue to follow
    [SerializeField] private Sprite characterIcon;
    [SerializeField][TextArea] private string dialogueText; // Name of Option


    public string SpeakingCharacter => speakingCharacter;
    public Sprite CharacterIcon => characterIcon;
    public string DialogueText => dialogueText;

}
