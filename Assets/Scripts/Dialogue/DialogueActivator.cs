using UnityEngine;
using System.Collections;

public class DialogueActivator : MonoBehaviour //, IInteractable
{

    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private CharacterMovement player;
    [SerializeField] private bool instantTrigger;
    [SerializeField] private bool walkInTrigger;
    [SerializeField] private bool mouseTrigger;
    [SerializeField] private bool oneTimeTrigger;

    private Collider2D boxCollider;

    public void UpdateDialogueObject(DialogueObject dialogueObject) {
        this.dialogueObject = dialogueObject;
    }


    private IEnumerator Start()
    {

        if (TryGetComponent(out Collider2D col))
        {
            boxCollider = col;
        }

        yield return new WaitForSeconds(0.5f);
        if (instantTrigger)
        {
            StartDialogue();
        }
    }

    // Trigger if collider is present and object clicked
    private void OnMouseUp()
    {
        if (mouseTrigger)
        {
            StartDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (walkInTrigger && other.gameObject.tag == "Player")
        {
            StartDialogue();
        }
    }

    public void StartDialogue()
    {
        // only start dialogue if there is no other dialogue open
        if (!player.DialogueUI.IsOpen)
        {
            if(TryGetComponent(out DialogueResponseEvents responseEvents)) {
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
            }

            if(TryGetComponent(out DialogueQuestionEvent questionEvent)) { //&& questionEvent.DialogueObject == dialogueObject
                player.DialogueUI.AddQuestionEvent(questionEvent.QuestionEvent);
            }

            if(TryGetComponent(out DialogueCloseEvent closeEvent)) { //&& questionEvent.DialogueObject == dialogueObject
                player.DialogueUI.AddCloseEvent(closeEvent.CloseEvent);
            }

            player.DialogueUI.ShowDialogue(dialogueObject);

            // if Dialogue shall not be triggered more than once, disable collider afterwards
            if (oneTimeTrigger && boxCollider)
            {
                boxCollider.enabled = false;
            }
        }
    }
}
