using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopMenueMusic : MonoBehaviour
{
    public bool Menue = false;
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0)) { 
            playMusic.MenueActive = Menue;
            playMusic.menueMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
