using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO.Events;

/// <summary>
/// Script for handling anything related to the instantiated AR objects in real world.
/// </summary>


public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject PlayGroundPrefab; //The playground prefab to be instantiated in real world
    [SerializeField] GameObject ballPrefab; //The ball prefab to be instantiated in real world
    [SerializeField] GameObject TransparentBall; //A transparent ball object for guidance on where the ball will be

    //scale and rotation sliders for setting the instantiated playground
    [SerializeField] GameObject ScaleSlider;
    [SerializeField] GameObject RotationSlider;

    [SerializeField] GameObject TapToPlaceBallTxt;
    [SerializeField] GameObject SetTransformBtn; //button to start the game after setting the PG(playground) transform

    #region Events
    [SerializeField] EventSO FingerTouhcedEvent; //raised when the user touches the screen
    [SerializeField] EventSO FingerReleasedEvent; //raised when the user touch is released from the screen
    [SerializeField] EventSO PGPlacedEvent; //raised when th PG is placed in real world environment
    [SerializeField] EventSO BallBlacedEvent; //raised when the ball is placed in real world environment
    #endregion

    [HideInInspector]
    public GameObject InstantiatedPlayGround; //the instantiated PG object
    [HideInInspector]
    public GameObject InstantiatedBall; //the instantiated ball object
    [HideInInspector]
    public bool isPlaneDetected = false;

    private PlacementIndicator PlacementIndicator;

    #region booleans for controlling the level sequence
    bool fingerReleased = false;
    bool fingerTouched = false;
    bool isPlayGroundPlaced = false;
    bool isBallPlaced = false;
    bool canShoot = false;
    bool canInput = true;
    #endregion

    void Start()
    {
        PlacementIndicator = FindObjectOfType<PlacementIndicator>();
    }

    void Update()
    {
        if (canInput) //This is false only when the PG scale and rotation UI is shown to avoid placing the ball by mistake
        {
            fingerTouched = Input.GetMouseButtonDown(0);
            fingerReleased = Input.GetMouseButtonUp(0);


            if (fingerReleased)
            {
                FingerReleasedEvent.Raise();
            }

            if (fingerTouched)
            {
                FingerTouhcedEvent.Raise();
            }
        }

        #region old method, kept for the record
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
        #endregion
    }



    #region Ball shooting functionaltiy
    /// <summary>
    /// Shoots the ball in its forward direction. Referenced in the inspector on listener on OnFingerReleasedEvent
    /// </summary>
    public void ShootBall()
    {
        if (canShoot)
        {
            InstantiatedBall.transform.GetChild(0).gameObject.SetActive(false); //deactivate the guiding arrow
            InstantiatedBall.GetComponent<Rigidbody>().AddForce(InstantiatedBall.transform.forward * 3.5f, ForceMode.Impulse);

            //to allow for multiple ball placing
            isBallPlaced = false;
            canShoot = false;
        }
    }

    #region CanShoot bool setting
    /// <summary>
    /// To give some time before setting the CanShoot bool to true, otherwise the ball will be thrown immediately after it's placed. Referenced in the inspector on listener on OnBallPlacedEvent
    /// </summary>
    public void DecideShootState()
    {
        Invoke("SetCanShoot", 0.3f); //delay the call of SetCanShoot() by 0.3 seconds
    }

    private void SetCanShoot()
    {
        canShoot = true;
    }
    #endregion 
    #endregion


    #region AR objects placement in real world

    /// <summary>
    /// Place ball in the real world environement. Called everytime a finger touches the screen. Referenced in the inspector on listener on OnFingerTouchedEvent
    /// </summary>
    public void PlaceBall()
    {
        if (!isBallPlaced && isPlayGroundPlaced) //if the playground is placed and there is no ball placed
        {
            InstantiatedBall = Instantiate(ballPrefab, PlacementIndicator.transform.position, PlacementIndicator.transform.rotation); //instantiate ball
            InstantiatedBall.transform.LookAt(InstantiatedPlayGround.transform.GetChild(0)); //set default rotation of the ball (looking at the goal)
            isBallPlaced = true;
            BallBlacedEvent.Raise();
        }
    }

    /// <summary>
    /// Place PG in the real world environement. Called everytime a finger touches the screen. Referenced in the inspector on listener on OnFingerTouchedEvent
    /// </summary>
    public void PlacePlayGround()
    {
        if (!isPlayGroundPlaced && isPlaneDetected) //if a plane is detected and the playground isn't placed
        {
            InstantiatedPlayGround = Instantiate(PlayGroundPrefab, PlacementIndicator.transform.position, PlacementIndicator.transform.rotation); //instantiate PG
            isPlayGroundPlaced = true;
            PGPlacedEvent.Raise();
        }
    }
    #endregion

    #region Hints

    /// <summary>
    /// Hides AR placement guides. Called on listener on OnBallPlacedEvent in the inspector
    /// </summary>
    public void HideHints()
    {
        TransparentBall.SetActive(false);
        TapToPlaceBallTxt.SetActive(false);
        PlacementIndicator.gameObject.SetActive(false);
    }

    /// <summary>
    /// Shows AR placement guides. Called on listener on OnPGTransformSetEvent in the inspector
    /// </summary>
    public void ShowHints()
    {
        TransparentBall.SetActive(true);
        TapToPlaceBallTxt.SetActive(true);
        PlacementIndicator.gameObject.SetActive(true);
    }
    #endregion


    /// <summary>
    /// Shows or hides the PG scale and rotation UI according to "ShowState". Called on Listener on PGPlacedEvent in the inspector and on the button "SetTransform"
    /// </summary>
    /// <param name="showState"></param>
    public void PlayGroundTransformUI(bool showState)
    {
        canInput = !showState; //no input can be recieved if the UI is shown except for the UI controls, and vice versa
        PlacementIndicator.gameObject.SetActive(showState);
        ScaleSlider.SetActive(showState);
        RotationSlider.SetActive(showState);
        SetTransformBtn.SetActive(showState);
    }



}
