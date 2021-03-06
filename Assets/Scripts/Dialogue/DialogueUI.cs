using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{

    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private GameObject characterNameBox;
    [SerializeField] private GameObject nextButton;

    private ResponseHandler responseHandler;
    private QuestionHandler questionHandler;
    private CloseEvent closeEvent;
    private TypewriterEffect typewriterEffect;
    private bool nextClicked = false;

    public bool IsOpen { get; private set; }

    private void Start()
    {

        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        questionHandler = GetComponent<QuestionHandler>();

        // register nextButton click event
        nextButton.GetComponent<Button>().onClick.AddListener(() => OnNextClicked());

        // reset dialogueBox
        CloseDialogueBox();
    }

    // disable character movement while in dialogue, show dialogue box and show first dialogue
    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);

        // set CharacterName if given
        if (!string.IsNullOrEmpty(dialogueObject.CharacterName))
        {
            characterNameBox.SetActive(true);
            characterName.text = dialogueObject.CharacterName;
        }

        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents) {
        responseHandler.AddResponseEvents(responseEvents);
    }

    public void AddQuestionEvent(QuestionEvent questionEvent) {
        questionHandler.AddQuestionEvent(questionEvent);
    }

    public void AddCloseEvent(CloseEvent closeEvent) {
        this.closeEvent = closeEvent;
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {

        // return dialogues in dialogueObject one after another
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            nextClicked = false;
            nextButton.gameObject.SetActive(true);

            // get current dialogue text 
            string dialogue = dialogueObject.Dialogue[i];

            // start coroutine with typing
            yield return RunTypingEffect(dialogue);
            textLabel.text = dialogue;

            // get out of the loop of displaying the dialogue if there is no more dialogue to show or there are responses
            if (i == dialogueObject.Dialogue.Length - 1 && (dialogueObject.HasResponses || !string.IsNullOrEmpty(dialogueObject.Question.Answer))) break;

            // wait for next to be clicked
            yield return null;
            yield return new WaitUntil(() => nextClicked == true);
        }

        // if there are any responses, display them instead of closing
        if (dialogueObject.HasResponses)
        {
            Debug.Log("HasResponse");
            nextButton.gameObject.SetActive(false);
            responseHandler.ShowResponses(dialogueObject.Responses);
        }

        else if (!string.IsNullOrEmpty(dialogueObject.Question.Answer))
        {
            Debug.Log("HasQuestion");
            nextButton.gameObject.SetActive(false);
            questionHandler.ShowQuestion(dialogueObject.Question);
        }
        else
        {
            // close at the end
            CloseDialogueBox();
        }
    }


    private IEnumerator RunTypingEffect(string dialogue)
    {
        // writing effect until next is clicked
        typewriterEffect.Run(dialogue, textLabel);
        while (typewriterEffect.IsRunning)
        {
            yield return null;

            if (nextClicked == true)
            {
                typewriterEffect.Stop();
                nextClicked = false;
            }
        }
    }

    public void CloseDialogueBox()
    {
        // hide dialogueBox if dialogue is finished and empty text, allow character movement
        IsOpen = false;
        dialogueBox.SetActive(false);
        characterNameBox.SetActive(false);
        textLabel.text = string.Empty;
        characterName.text = string.Empty;

        if(closeEvent!= null) {
            closeEvent.OnClose?.Invoke();
        }
        closeEvent = null;
    }

    private void OnNextClicked()
    {
        print("button clicked");
        FMODUnity.RuntimeManager.PlayOneShot("event:/Menu_Button");
        nextClicked = true;
    }
}
