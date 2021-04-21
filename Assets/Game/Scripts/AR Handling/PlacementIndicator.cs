using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{

    private ARRaycastManager aRRaycastManager;
    private GameObject placementIndicator;
    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        placementIndicator = this.transform.GetChild(0).gameObject;

        placementIndicator.SetActive(false);
    }

    void Update()
    {
        PlaceIndicator();
    }

    private void PlaceIndicator()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            this.transform.position = hits[0].pose.position;
            this.transform.rotation = hits[0].pose.rotation;

            if (!placementIndicator.activeInHierarchy)
                placementIndicator.SetActive(true);
        }
    }
}
