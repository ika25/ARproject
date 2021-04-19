using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject PlayGroundPrefab;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject ScaleSlider;
    [SerializeField] GameObject ShootBtn;
    [SerializeField] GameObject TapToPlaceBallTxt;
    [SerializeField] GameObject TransparentBall;

    [HideInInspector]
    public GameObject InstantiatedPlayGround;
    [HideInInspector]
    public GameObject InstantiatedBall;
    private PlacementIndicator PlacementIndicator;
    bool fingerTouched = false;
    bool isPrefabPresent = false;
    bool isBallPlaced = false;
    void Start()
    {
        PlacementIndicator = FindObjectOfType<PlacementIndicator>();
        ScaleSlider.SetActive(false);

    }

    void Update()
    {
        fingerTouched = Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began;
        if (!isPrefabPresent)
        {

            if (fingerTouched)
            {
                InstantiatedPlayGround = Instantiate(PlayGroundPrefab, PlacementIndicator.transform.position, PlacementIndicator.transform.rotation);
                
                isPrefabPresent = true;
            } 
        }

        else if(!isBallPlaced)
        {

            if(!fingerTouched)
            {
                if (!ScaleSlider.activeInHierarchy)
                    ScaleSlider.SetActive(true);

                if (!TransparentBall.activeInHierarchy)
                TransparentBall.SetActive(true);

                if(!TapToPlaceBallTxt.activeInHierarchy)
                TapToPlaceBallTxt.SetActive(true);
            }
            else
            {
                isBallPlaced = true;
                ScaleSlider.SetActive(false);
                TransparentBall.SetActive(false);
                TapToPlaceBallTxt.SetActive(false);
                PlacementIndicator.gameObject.SetActive(false);
                ShootBtn.SetActive(true);
                InstantiatedBall = Instantiate(ballPrefab, PlacementIndicator.transform.position, PlacementIndicator.transform.rotation);
            }
        }
    }
}
