using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openHub : MonoBehaviour
{
    public static bool hubOpen;
    [SerializeField] private GameObject background;
    [SerializeField] private CharacterMovement character;
    private List<Transform> children;

    // Start is called before the first frame update
    void Start()
    {
        hubOpen = false;
    }

    void OnMouseUp()
    {
        // only open hub if no dialogue is active
        if (!character.DialogueUI.IsOpen)
        {
            background.SetActive(!background.activeSelf);
            hubOpen = !hubOpen;
            // disable movement while in hub
            character.StopMovement = !character.StopMovement;

            if (hubOpen == true)
            {
                // steckbrief, schwert, brustblech, maskenhelm, holzkaestchen, krug, krug2
                foreach (Transform child in background.transform)
                {
                    if (GameState.FulfilledRequirements.Contains(child.gameObject.name))
                    {
                        Debug.Log(child.gameObject.name);
                        child.gameObject.SetActive(true);
                    }
                }
            }



        }


    }

}
