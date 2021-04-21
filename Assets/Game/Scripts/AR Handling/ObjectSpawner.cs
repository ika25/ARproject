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
    [SerializeField] GameObject SetTransformBtn;

    [SerializeField] EventSO FingerTouhcedEvent;
    [SerializeField] EventSO FingerReleasedEvent;
    [SerializeField] EventSO PGBlacedEvent;
    //[SerializeField] EventSO PGTransformSetEvent;
    [SerializeField] EventSO BallBlacedEvent;
    //[SerializeField] EventSO BallShotEvent;

    [HideInInspector]
    public GameObject InstantiatedPlayGround;
    [HideInInspector]
    public GameObject InstantiatedBall;
    [HideInInspector]
    public bool isPlaneDetected = false;

    private PlacementIndicator PlacementIndicator;

    bool fingerReleased = false;
    bool fingerTouched = false;
    bool isPlayGroundPlaced = false;
    bool isBallPlaced = false;
    bool canShoot = false;
    //bool PlayGroundTransformSet = false;
    //bool isDecided = false;
    void Start()
    {
        PlacementIndicator = FindObjectOfType<PlacementIndicator>();
    }

    void Update()
    {
        fingerTouched = Input.GetMouseButtonDown(0); /*Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began;*/
        fingerReleased = Input.GetMouseButtonUp(0);


        if (fingerReleased)
        {
            FingerReleasedEvent.Raise();
        }

        if (fingerTouched)
        {
            FingerTouhcedEvent.Raise();
        }


        //if (!isPlayGroundPlaced)
        //{

        //    if (fingerTouched)
        //    {
        //        PlacePlayGround();

        //        isPlayGroundPlaced = true;
        //    }
        //}

        //else if (!isBallPlaced)
        //{

        //    if (!fingerTouched)
        //    {
        //        ShowHints();
        //    }
        //    else
        //    {
        //        HideHints();
        //        PlaceBall();

        //        isBallPlaced = true;
        //    }
        //}

        //if (PlayGroundTransformSet)
        //{
        //    if (fingerReleased)
        //    {
        //        if (isBallPlaced)
        //        {
        //            Invoke("SetCanShoot", 0.3f);
        //        }
        //        else
        //        {
        //            canShoot = false;
        //        }
        //    }

        //    if (fingerReleased && canShoot == true)
        //    {
        //        ShootBall();
        //    }
        //}
    }

    public void DecideShootState()
    {
        Invoke("SetCanShoot", 0.3f);
    }

    public void ShootBall()
    {
        if (canShoot)
        {
            InstantiatedBall.transform.GetChild(0).gameObject.SetActive(false);
            InstantiatedBall.GetComponent<Rigidbody>().AddForce(InstantiatedBall.transform.forward * 4, ForceMode.Impulse);
            isBallPlaced = false;
            canShoot = false;
            //BallShotEvent.Raise();
        }
    }

    #region AR objects placement in real world
    public void PlaceBall()
    {
        if (!isBallPlaced && isPlayGroundPlaced)
        {
            InstantiatedBall = Instantiate(ballPrefab, PlacementIndicator.transform.position, PlacementIndicator.transform.rotation);
            InstantiatedBall.transform.LookAt(InstantiatedPlayGround.transform.GetChild(0));
            isBallPlaced = true;
            BallBlacedEvent.Raise();
        }
    }

    public void PlacePlayGround()
    {
        if (!isPlayGroundPlaced && isPlaneDetected)
        {
            InstantiatedPlayGround = Instantiate(PlayGroundPrefab, PlacementIndicator.transform.position, PlacementIndicator.transform.rotation);
            isPlayGroundPlaced = true;
            PGBlacedEvent.Raise();
        }
    }
    #endregion

    #region Hints
    public void HideHints()
    {
        //if (!fingerTouched)
        {
            TransparentBall.SetActive(false);
            TapToPlaceBallTxt.SetActive(false);
            PlacementIndicator.gameObject.SetActive(false);
        }
    }

    public void ShowHints()
    {
        //if (!fingerTouched)
        {
            //if (!TransparentBall.activeInHierarchy)
            TransparentBall.SetActive(true);

            //if (!TapToPlaceBallTxt.activeInHierarchy)
            TapToPlaceBallTxt.SetActive(true);
            PlacementIndicator.gameObject.SetActive(true);
        }
    }
    #endregion

    public void PlayGroundTransformUI(bool showState)
    {
        PlacementIndicator.gameObject.SetActive(false);
        ScaleSlider.SetActive(showState);
        RotationSlider.SetActive(showState);
        SetTransformBtn.SetActive(showState);
    }


    private void SetCanShoot()
    {
        canShoot = true;
    }
}
