using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SimpleRaycasting : MonoBehaviour
{

    public ARRaycastManager raycastManager;
    public GameObject prefabToBePlaced;
    public Camera raycastCamera;

    private GameObject _objectInstance;
    private List<ARRaycastHit> _raycastHits = new List<ARRaycastHit>();


    // Update is called once per frame
    void Update()
    {
        if (raycastManager == null || raycastCamera == null) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
            if (raycastManager.Raycast(ray, _raycastHits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
            {

                Pose pose = _raycastHits[0].pose;

                if (_objectInstance == null)
                {
                    _objectInstance = Instantiate<GameObject>(prefabToBePlaced, pose.position, pose.rotation);
                }
                else
                {
                    _objectInstance.transform.SetPositionAndRotation(pose.position, pose.rotation);
                }
            }
        }

    }
}
