using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class walkingSound : MonoBehaviour
{
    private FMOD.Studio.EventInstance footstepInstance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayFootstep()
    {
            footstepInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Footsteps");
            footstepInstance.start();
            footstepInstance.release();

    }
}
