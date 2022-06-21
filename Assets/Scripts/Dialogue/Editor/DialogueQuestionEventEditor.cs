using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialogueQuestionEvent))]
public class DialogueQuestionEventEditor : Editor
{
    // Button to refresh dialogueQuestionEvent-View
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueQuestionEvent questionEvent = (DialogueQuestionEvent)target;

        if(GUILayout.Button("Refresh")) {
            questionEvent.OnValidate();
        }
    }
}
