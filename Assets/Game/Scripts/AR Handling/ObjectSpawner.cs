using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO.Events;
public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject PlayGroundPrefab;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject TransparentBall;
    [SerializeField] GameObject ScaleSlider;
    [SerializeField] GameObject RotationSlider;
    [SerializeField] GameObject TapToPlaceBallTxt;

    [SerializeField] EventSO FingerTouhcedEvent;
    [SerializeField] EventSO FingerReleasedEvent;
    

    [HideInInspector]
    public GameObject InstantiatedPlayGround;
    [HideInInspector]
    public GameObject InstantiatedBall;

    private PlacementIndicator PlacementIndicator;

    bool fingerReleased = false;
    bool fingerTouched = false;
    bool isPlayGroundPlaced = false;
    bool isBallPlaced = false;
    bool canShoot = false;
    bool PlayGroundTransformSet = false;
    void Start()
    {
        PlacementIndicator = FindObjectOfType<PlacementIndicator>();
    }

    void Update()
    {
        fingerTouched = Input.GetMouseButtonDown(0); /*Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began;*/
        fingerReleased = Input.GetMouseButtonUp(0);


        if(fingerReleased)
        {
            FingerReleasedEvent.Raise();
        }

        if(fingerTouched)
        {
            FingerTouhcedEvent.Raise();
        }


        if (!isPlayGroundPlaced)
        {

            if (fingerTouched)
            {
                PlacePlayGround();

                isPlayGroundPlaced = true;
            }
        }

        else if(!isBallPlaced)
        {

            if(!fingerTouched)
            {
                ShowHints();
            }
            else
            {
                HideHints();
                PlaceBall();

                isBallPlaced = true;
            }
        }

        if (PlayGroundTransformSet)
        {
            if (fingerReleased && isPlayGroundPlaced)
            {
                if (isBallPlaced)
                {
                    Invoke("SetCanShoot", 0.3f);
                }
                else
                {
                    canShoot = false;
                }
            }

            if (fingerReleased && canShoot == true)
            {
                ShootBall();
            } 
        }
    }



    private void ShootBall()
    {
        InstantiatedBall.transform.GetChild(0).gameObject.SetActive(false);
        InstantiatedBall.GetComponent<Rigidbody>().AddForce(InstantiatedBall.transform.forward * 4, ForceMode.Impulse);
    }

    #region AR objects placement in real world
    private void PlaceBall()
    {
        InstantiatedBall = Instantiate(ballPrefab, PlacementIndicator.transform.position, PlacementIndicator.transform.rotation);
        InstantiatedBall.transform.LookAt(InstantiatedPlayGround.transform.GetChild(0));
    }

    private void PlacePlayGround()
    {
        InstantiatedPlayGround = Instantiate(PlayGroundPrefab, PlacementIndicator.transform.position, PlacementIndicator.transform.rotation);
    }
    #endregion

    #region Hints
    private void HideHints()
    {
        TransparentBall.SetActive(false);
        TapToPlaceBallTxt.SetActive(false);
        PlacementIndicator.gameObject.SetActive(false);
    }

    private void ShowHints()
    {
        if (!TransparentBall.activeInHierarchy)
            TransparentBall.SetActive(true);

        if (!TapToPlaceBallTxt.activeInHierarchy)
            TapToPlaceBallTxt.SetActive(true);
    } 
    #endregion

    private void PlayGroundTransformUI(bool showState)
    {
        ScaleSlider.SetActive(showState);
        RotationSlider.SetActive(showState);
    }


    private void SetCanShoot()
    {
        canShoot = true;
    }
}
