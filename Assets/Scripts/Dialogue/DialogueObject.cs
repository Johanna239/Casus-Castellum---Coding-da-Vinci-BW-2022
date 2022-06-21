using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] private string characterName;
    [SerializeField][TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;
    [SerializeField] private Question question;

    public string CharacterName => characterName;

    public string[] Dialogue => dialogue;

    // only true if the response array exists and is filled
    public bool HasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;

    public Question Question => question;
}
