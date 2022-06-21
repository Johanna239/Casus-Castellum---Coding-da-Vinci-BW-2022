using UnityEngine;


public class DialogueQuestionEvent : MonoBehaviour
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private QuestionEvent questionEvent;

    public DialogueObject DialogueObject => dialogueObject;
    public QuestionEvent QuestionEvent => questionEvent;

    public void OnValidate()
    {
        if (dialogueObject == null) return;
        if (dialogueObject.Question == null) return;
        // if (questionEvent != null) return;

        Question question = dialogueObject.Question;

        if(questionEvent != null) {
            questionEvent.eventName = question.Answer;
        }
        else{
            questionEvent = new QuestionEvent() {eventName = question.Answer};
        }
    }
}
