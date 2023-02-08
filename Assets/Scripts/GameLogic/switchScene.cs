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
        checkSceneTransition(levelname);
    }

    public void checkSceneTransition(string levelToGo)
    {
        if (requirements.Length > 0)
        {
            transitionAllowed = true;

            foreach (string requirement in requirements)
            {
                if (!GameState.FulfilledRequirements.Contains(requirement))
                {
                    transitionAllowed = false;
                    Debug.Log("requirement " + requirement + " not met for transition");
                }
            }
        }

        if (transitionAllowed)
        {
            if (levelToGo == "Quit")
            {
                quitGame();
            }
            else
            {
                SceneManager.LoadScene(levelToGo);
            }
        }
        else
        {
            if (TryGetComponent(out DialogueActivator activator))
            {
                activator.StartDialogue();
            }
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void loadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
