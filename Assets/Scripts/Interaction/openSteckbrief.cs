using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openSteckbrief : MonoBehaviour
{
    public static bool steckbriefOffen = false;
    public static FMOD.Studio.EventInstance steckbriefStimme;

    // Start is called before the first frame update
    void Start()
    {
        steckbriefStimme = FMODUnity.RuntimeManager.CreateInstance(
            "event:/Sprache/Haben Sie dieses Schwert"
        );
    }

    // Update is called once per frame
    void Update() { }

    void OnMouseDown()
    {
        steckbriefOffen = true;
        GameState.fulfilledRequirement("steckbrief");
        steckbriefStimme.start();
        GameObject.Find("steckbrief").GetComponent<grosserSteckbrief>().Update();
        FMODUnity.RuntimeManager.PlayOneShot("event:/BUtton2paper");
        Destroy(gameObject);
    }
}
