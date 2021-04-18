using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject ObjToSpawn;
    private PlacementIndicator PlacementIndicator;
    bool fingerTouched = false;
    void Start()
    {
        PlacementIndicator = FindObjectOfType<PlacementIndicator>();
    }

    void Update()
    {
        fingerTouched = Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began;

        if (fingerTouched)
        {
            GameObject obj = Instantiate(ObjToSpawn, PlacementIndicator.transform.position, PlacementIndicator.transform.rotation);
        }
    }
}
