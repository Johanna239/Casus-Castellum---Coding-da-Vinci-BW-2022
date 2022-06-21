using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CloseEvent
{
    [HideInInspector] public string eventName;
    [SerializeField] private UnityEvent onClose;

    public UnityEvent OnClose => onClose;
}
