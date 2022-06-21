using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlaceSelectMenue : MonoBehaviour
{
    [SerializeField] private bool menue = false;
    [SerializeField] private GameObject inMuseumButton;
    [SerializeField] private GameObject outMuseumButton;

    [Header("Set Level Name")]
    [SerializeField] private string levelname = "Level_1_0";

    private void Start() {
        inMuseumButton.GetComponent<Button>().onClick.AddListener(() => OnButtonClicked(inMuseumButton.name));
        outMuseumButton.GetComponent<Button>().onClick.AddListener(() => OnButtonClicked(outMuseumButton.name));
    }

    private void OnButtonClicked(string buttonName) {

        switch(buttonName) {
            case "MuseumButton":
                GameState.InMuseum = true;
                break;
            case "OthePlaceButton":
                GameState.InMuseum = false;
                break;  
            default:
                print ("PlaceSelectButton not recognized");
                break;      
        }

        playMusic.MenueActive = menue;
        playMusic.menueMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        SceneManager.LoadScene(levelname);
    }

}
