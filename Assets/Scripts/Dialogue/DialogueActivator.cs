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
    private DialogueResponseEvents[] responseEvents;
    private DialogueQuestionEvent[] questionEvents;
    private DialogueCloseEvent[] closeEvents;

    public bool WalkInTrigger { get => walkInTrigger; set => walkInTrigger = value; }
    public bool MouseTrigger { get => mouseTrigger; set => mouseTrigger = value; }
    public bool OneTimeTrigger { get => oneTimeTrigger; set => oneTimeTrigger = value; }

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }


    private IEnumerator Start()
    {

        if (TryGetComponent(out Collider2D col))
        {
            boxCollider = col;
        }
        responseEvents = GetComponents<DialogueResponseEvents>();
        questionEvents = GetComponents<DialogueQuestionEvent>();
        closeEvents = GetComponents<DialogueCloseEvent>();

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
        if (WalkInTrigger && other.gameObject.tag == "Player")
        {
            StartDialogue();
        }
    }

    public void StartDialogue()
    {
        // only start dialogue if there is no other dialogue open
        if (!player.DialogueUI.IsOpen)
        {
            // set reference to current activator to later check for events
            player.DialogueUI.lastActivator = this;
            CheckEvents();

            player.DialogueUI.ShowDialogue(dialogueObject);

            // if Dialogue shall not be triggered more than once, disable collider afterwards
            if (oneTimeTrigger && boxCollider)
            {
                boxCollider.enabled = false;
            }
        }
    }

    public void CheckEvents()
    {
        // check all responseEvents for match
        if (responseEvents.Length > 0)
        {
            for (int i = 0; i < responseEvents.Length; i++)
            {
                if (responseEvents[i].DialogueObject == dialogueObject)
                {
                    player.DialogueUI.AddResponseEvents(responseEvents[i].Events);
                }
            }
        }

        if (questionEvents.Length > 0)
        {
            for (int i = 0; i < questionEvents.Length; i++)
            {
                if (questionEvents[i].DialogueObject == dialogueObject)
                {
                    player.DialogueUI.AddQuestionEvent(questionEvents[i].QuestionEvent);
                }
            }
        }

        if (closeEvents.Length > 0)
        {
            for (int i = 0; i < closeEvents.Length; i++)
            {
                if (closeEvents[i].DialogueObject == dialogueObject)
                {
                    player.DialogueUI.AddCloseEvent(closeEvents[i].CloseEvent);
                }
            }
        }

    }
}
