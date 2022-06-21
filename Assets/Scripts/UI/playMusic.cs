using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMusic : MonoBehaviour
{
    public static bool MenueActive;
    public static FMOD.Studio.EventInstance menueMusic;

    // Start is called before the first frame update

    void Start()
    {
        MenueActive = true;
        menueMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Musik/Menu2");
        menueMusic.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!MenueActive)
        {
            menueMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
