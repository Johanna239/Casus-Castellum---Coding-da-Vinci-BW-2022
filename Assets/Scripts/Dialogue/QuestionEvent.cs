using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class QuestionEvent
{
    [HideInInspector] public string eventName;
    [SerializeField] private UnityEvent onAnsweredQuestion;

    public UnityEvent OnAnsweredQuestion => onAnsweredQuestion;
}

