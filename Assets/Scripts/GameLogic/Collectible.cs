using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour
{
    [SerializeField] private string objectName;
    [SerializeField] private UnityEvent onCollectionEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseUp() {
        Debug.Log("collecting " + objectName);
        GameState.fulfilledRequirement(objectName);
        if (onCollectionEvent != null)
        {
            onCollectionEvent.Invoke();
        }
    }

}
