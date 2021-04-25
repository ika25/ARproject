using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script for handling the user's touch input
/// </summary>

public class SwipeInputManager : MonoBehaviour
{

    [SerializeField] ObjectSpawner objectSpawner;
    [SerializeField] Camera ARCam;
    Vector3 ThrowDirection;
    void Start()
    {
        ThrowDirection = Vector3.zero; //initial ball throw direction
    }

    void Update()
    {

        if (Input.GetMouseButton(0)) //this works for both mouse buttons and finger touches
        {
            RotateBall(); //rotate ball when a finger is holding on the screen
        }

    }


    /// <summary>
    /// Rotates the ball according to the user's touch position projected on the game's world space
    /// </summary>
    private void RotateBall()
    {
        ThrowDirection = GetSecondPoint(); //get the projection of the user's touch position on the game's world space

        objectSpawner.InstantiatedBall.transform.LookAt(ThrowDirection); //rotate the ball according the touch point
    }

    private Vector3 GetSecondPoint()
    {
        Ray camRay = ARCam.ScreenPointToRay(Input.mousePosition); //raycast from the mobile cam at the user's touch position on the screen
        if (Physics.Raycast(camRay, out RaycastHit hit)) //if any thing is hit (found by the raycast)
        {
            return hit.point; //return the hit point in the game's world space
        }
        return Vector3.zero; //if not, return the default ball rotation
    }
}
