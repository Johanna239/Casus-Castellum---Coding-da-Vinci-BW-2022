using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ShowPlacementIndicator : MonoBehaviour
{

        public ARRaycastManager raycastManager;
        public Camera raycastCamera;
        public GameObject poseMarker;
        private List<ARRaycastHit> _raycastHits = new List<ARRaycastHit>();
        private Pose pose;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkPose();
        updatePlacement();
        
    }

    private void updatePlacement()
    {
        if(pose != null) {
            poseMarker.SetActive(true);
            poseMarker.transform.SetPositionAndRotation(pose.position, pose.rotation);
        } else {
            poseMarker.SetActive(false);
        }
    }

    private void checkPose()
    {
        if (raycastManager == null || raycastCamera == null) { return; }
    	
        var center = raycastCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        Ray ray = raycastCamera.ScreenPointToRay(center);

        if (raycastManager.Raycast(ray, _raycastHits, UnityEngine.XR.ARSubsystems.TrackableType.Planes)) {
            pose = _raycastHits[0].pose;

            var camDirection = new Vector3(raycastCamera.transform.forward.x, 0, raycastCamera.transform.forward.z).normalized;
            pose.rotation = Quaternion.LookRotation(camDirection);
        }

    }
}
