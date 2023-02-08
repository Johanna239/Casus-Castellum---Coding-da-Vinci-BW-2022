using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI dialogueUI;
    private ResponseEvent[] responseEvents;
    private List<GameObject> tempResponseButtons = new List<GameObject>();

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        this.responseEvents = responseEvents;
    }

    public void ShowResponses(Response[] responses)
    {
        float responseBoxHeight = 0f;
        int count = 0;

        // instantiate as many responseButtons as there are responses
        for (int i = 0; i < responses.Length; i++)
        {
            Response response = responses[i];
            int responseIndex = i;

            count++;

            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);

            // set name of Button to optionName (name of the response)
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;

            // event callback if button is clicked
            responseButton.GetComponent<Button>().onClick.AddListener(() => onPickedResponse(response, responseIndex));

            // add buttons to list for later reference
            tempResponseButtons.Add(responseButton);

            // increase size of box after two added responses
            if (count % 2 == 1)
            {
                responseBoxHeight += responseButtonTemplate.sizeDelta.y;
            }
        }

        // set final size of responseBox and show it
        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    private void onPickedResponse(Response response, int responseIndex)
    {
        // disable responseOptions
        responseBox.gameObject.SetActive(false);

        // destroy all current optionButtons and clear list
        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }
        tempResponseButtons.Clear();

        // if there are any events associated with clicked response, call them
        if (responseEvents != null && responseIndex <= responseEvents.Length)
        {
            responseEvents[responseIndex].OnPickedResponse?.Invoke();
        }

        // reset responseEvents
        responseEvents = null;

        if (response.DialogueObject)
        {
            // show dialogue depending on clicked response
            dialogueUI.ShowDialogue(response.DialogueObject);
            dialogueUI.lastActivator.UpdateDialogueObject(response.DialogueObject);
            dialogueUI.lastActivator.CheckEvents();
            
        }
        else 
        {
            dialogueUI.CloseDialogueBox();
        }

    }
}
