using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickOnSchwert : MonoBehaviour
{
    public static bool schwertGeklickt;
    [SerializeField] private DialogueObject outMuseumDialogue;
    private DialogueActivator dialogueActivator;

    // Start is called before the first frame update
    void Start()
    {
        schwertGeklickt = false;
        dialogueActivator = gameObject.GetComponent<DialogueActivator>();
    }

    // Update is called once per frame
    void Update() { }

    void OnMouseDown()
    {
            if(GameState.InMuseum == false) {
                dialogueActivator.UpdateDialogueObject(outMuseumDialogue);
            }
            schwertGeklickt = true;
            dialogueActivator.StartDialogue();
            Destroy(gameObject);
    }
}
