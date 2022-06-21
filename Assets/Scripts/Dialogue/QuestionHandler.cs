using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Text.RegularExpressions;

public class QuestionHandler : MonoBehaviour
{
    [SerializeField] private RectTransform questionBox;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text textHintOverlay;
    [SerializeField] private TMP_Text correctCharacterText;
    [SerializeField] private TMP_Text correctCharacterCount;
    [SerializeField] private Image feedbackImage;
    [SerializeField] private bool useFixedLength = false;
    [SerializeField] private int fixedLength = 10;
    [SerializeField] private bool showUnderscores;
    [SerializeField] private bool showCorrectLetters;
    [SerializeField] private string replaceWith;
    private int correctCharacters;

    private DialogueUI dialogueUI;
    private QuestionEvent questionEvent;

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();

        inputField.GetComponent<TMP_InputField>().lineType = TMP_InputField.LineType.MultiLineSubmit;

        //disable textHintOverlay by default (if it exists)
        if (textHintOverlay != null)
        {
            textHintOverlay.enabled = false;
        }

        // don't show feedback Image at the beginning (if it exists)
        if (feedbackImage != null)
        {
            feedbackImage.enabled = false;
        }

        // show correct letter hint only if option is toggled (true) (if Textobjects exist)
        if (!showCorrectLetters && correctCharacterText != null && correctCharacterCount != null)
        {
            correctCharacterText.enabled = false;
            correctCharacterCount.enabled = false;
        }
    }

    public void AddQuestionEvent(QuestionEvent questionEvent)
    {
        this.questionEvent = questionEvent;
    }

    public void ShowQuestion(Question question)
    {
        // if fixed length option disabled (default), use solution word length
        // It is not possible to type more than the given length
        if (useFixedLength)
        {
            inputField.characterLimit = fixedLength;
        }
        else
        {
            inputField.characterLimit = question.Answer.Length;
        }

        // add listener only if solution is given
        if (question.Answer != "")
        {
            inputField.onValueChanged.AddListener(delegate { ValueChangeCheck(question); });
            //inputField.onEndEdit.AddListener(delegate { ResetFocus(); });
        }

        // show textHintOverlay only if option is toggled (true) and overlay exists
        if (showUnderscores && textHintOverlay != null)
        {
            Debug.Log("Enabling Hints");
            Regex regex = new Regex("[^\\s]");
            textHintOverlay.text = regex.Replace(question.Answer, replaceWith);
            textHintOverlay.enabled = true;
        }

        questionBox.gameObject.SetActive(true);
    }

    public void ValueChangeCheck(Question question)
    {
        // convert input to lowercase and compare with solution
        // if they match: 
        if (inputField.text.ToLower() == question.Answer.ToLower())
        {
            // disable input field
            inputField.interactable = false;

            // show feedback image, if one exists
            if (feedbackImage != null)
            {
                feedbackImage.enabled = true;
            }

            // show feedback text if textfield is specified
            if (correctCharacterCount != null)
            {
                correctCharacterCount.text = "Alle Richtig!";
            }

            // yield return new WaitForSeconds(1);
            StartCoroutine(Celebrate(question));
        }
        else
        {
            // compare each typed character with solution
            // increase counter if characters match (same character, same position)
            correctCharacters = 0;
            for (int i = 0; i < inputField.text.Length; i++)
            {
                if (inputField.text.Length >= question.Answer.Length)
                {
                    return;
                }

                if (inputField.text[i].ToString().ToLower() == question.Answer[i].ToString().ToLower())
                {
                    correctCharacters++;
                }
            }

            // show correct characters if textfield is specified
            if (correctCharacterCount != null)
            {
                correctCharacterCount.text = correctCharacters.ToString();
            }
        }
    }

    private IEnumerator Celebrate(Question question)
    {
        yield return new WaitForSeconds(1);
        questionBox.gameObject.SetActive(false);
        inputField.interactable = true;
        feedbackImage.enabled = false;
        correctCharacterCount.text = "0";
        textHintOverlay.text = "";
        inputField.text = "";

        // if there are any events associated with clicked response, call them
        if (questionEvent != null)
        {
            questionEvent.OnAnsweredQuestion?.Invoke();
        }

        // reset responseEvents
        questionEvent = null;

        if (question.DialogueObject)
        {
            // show dialogue depending on clicked response
            dialogueUI.ShowDialogue(question.DialogueObject);
        }
        else
        {
            dialogueUI.CloseDialogueBox();
        }

    }

}
