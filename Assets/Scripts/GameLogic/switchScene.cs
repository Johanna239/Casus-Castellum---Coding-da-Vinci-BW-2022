using UnityEngine;
using UnityEngine.SceneManagement;

public class switchScene : MonoBehaviour
{

    [Header("Set Level Name")]
    [SerializeField] private string levelname = "Level_0_1";
    [SerializeField] public bool transitionAllowed = true;
    [SerializeField] public string[] requirements;

    void OnMouseUp()
    {

        if (requirements.Length > 0)
        {
            transitionAllowed = true;

            foreach (string requirement in requirements)
            {
                if (!GameState.FulfilledRequirements.Contains(requirement))
                {
                    transitionAllowed = false;
                    print("requirement " + requirement + "not met for transition");
                }
            }
        }


        if (transitionAllowed)
        {
            SceneManager.LoadScene(levelname);
        }
        else
        {
            if (TryGetComponent(out DialogueActivator activator))
            {
                activator.StartDialogue();
            }
        }



    }
}
