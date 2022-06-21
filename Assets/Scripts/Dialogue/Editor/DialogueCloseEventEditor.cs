using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialogueCloseEvent))]
public class DialogueCloseEventEditor : Editor
{
    // Button to refresh dialogueQuestionEvent-View
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueCloseEvent closeEvent = (DialogueCloseEvent)target;

        if(GUILayout.Button("Refresh")) {
            closeEvent.OnValidate();
        }
    }
}