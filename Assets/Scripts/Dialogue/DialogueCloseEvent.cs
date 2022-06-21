using UnityEngine;

public class DialogueCloseEvent : MonoBehaviour
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private CloseEvent closeEvent;

    public DialogueObject DialogueObject => dialogueObject;
    public CloseEvent CloseEvent => closeEvent;

    public void OnValidate()
    {
        if (dialogueObject == null) return;
        // if (closeEvent != null) return;

        if (closeEvent == null)
        {
            closeEvent = new CloseEvent() { eventName = "Closing" };
        }
    }
}
