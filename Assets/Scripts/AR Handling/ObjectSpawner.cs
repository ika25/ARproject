using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject ScaleSlider;

    public GameObject ObjToSpawn;
    [HideInInspector]
    public GameObject InstantiatedPlayGround;
    private PlacementIndicator PlacementIndicator;
    bool fingerTouched = false;
    bool isPrefabPresent = false;
    void Start()
    {
        PlacementIndicator = FindObjectOfType<PlacementIndicator>();
        ScaleSlider.SetActive(false);

    }

    void Update()
    {
        //if (!isPrefabPresent)
        {
            fingerTouched = Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began;

            if (fingerTouched)
            {
                InstantiatedPlayGround = Instantiate(ObjToSpawn, PlacementIndicator.transform.position, PlacementIndicator.transform.rotation);
                if (!ScaleSlider.activeInHierarchy)
                    ScaleSlider.SetActive(true);
                isPrefabPresent = true;
            } 
        }
    }
}
