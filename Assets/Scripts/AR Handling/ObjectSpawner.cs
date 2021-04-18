using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject ObjToSpawn;
    private PlacementIndicator PlacementIndicator;
    void Start()
    {
        PlacementIndicator = FindObjectOfType<PlacementIndicator>();
    }

    void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            GameObject obj = Instantiate(ObjToSpawn, PlacementIndicator.transform.position, PlacementIndicator.transform.rotation);
        }
    }
}
