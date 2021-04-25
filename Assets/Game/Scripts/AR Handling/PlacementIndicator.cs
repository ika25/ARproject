using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Script for handling the placement indicator. Placement indicator is used to guide the user on where the instantiated AR objects lie in the real world.
/// </summary>


public class PlacementIndicator : MonoBehaviour
{
    [SerializeField] ObjectSpawner ObjectSpawner;
    private ARRaycastManager aRRaycastManager; //required for raycasting in AR
    private GameObject placementIndicator; //the placement indicator to be rendered in real world, the child of the object holding this script
    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        placementIndicator = this.transform.GetChild(0).gameObject;

        placementIndicator.SetActive(false); //Initially deactivate the indicator until some planes are detected
    }

    void Update()
    {
        PlaceIndicator();
    }

    /// <summary>
    /// Place the indicator in real world in case a plane is detected by ARCore
    /// </summary>

    private void PlaceIndicator()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>(); //a list of the possible detected objects in the real world
        aRRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes); //raycast from the mobile's camera to real world environment, tracking any planes, and and any found ones to the list "hits"

        if (hits.Count > 0) //if any place is detected
        {
            ObjectSpawner.isPlaneDetected = true; //used in the objectspawner script to only place the playground in case a plane is detected

            //set position and rotation of the indicator to be equal to the nearest detected plane
            this.transform.position = hits[0].pose.position; 
            this.transform.rotation = hits[0].pose.rotation;

            if (!placementIndicator.activeInHierarchy)
                placementIndicator.SetActive(true); //activate the placement indicator
        }
        else
        {
            ObjectSpawner.isPlaneDetected = false; //used in the objectspawner script to only place the playground in case a plane is detected
        }
    }
}
