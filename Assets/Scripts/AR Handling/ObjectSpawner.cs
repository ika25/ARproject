using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject PlayGroundPrefab;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject ScaleSlider;
    //[SerializeField] GameObject ShootBtn;
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
    bool canShoot = false;
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
                //ShootBtn.SetActive(true);
                InstantiatedBall = Instantiate(ballPrefab, PlacementIndicator.transform.position, PlacementIndicator.transform.rotation);
                InstantiatedBall.transform.LookAt(InstantiatedPlayGround.transform.GetChild(0));
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            if(isBallPlaced)
            {
                Invoke("delayforInput", 0.3f);
            }
            else
            {
                canShoot = false;
            }
        }

        if (Input.GetMouseButtonUp(0) && canShoot == true)
        {
            InstantiatedBall.GetComponent<Rigidbody>().AddForce(InstantiatedBall.transform.forward * 4, ForceMode.Impulse);

        }    
    }

    private void delayforInput()
    {
        canShoot = true;
    }
}
