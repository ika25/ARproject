using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script for handling the Playground scale and rotation in runtime
/// </summary>


public class PlayGroundTransformUI : MonoBehaviour
{
    [SerializeField] ObjectSpawner ObjectSpawner;

    [SerializeField] Slider scaleSlider;   
    [SerializeField] Slider rotationSlider;

    Transform playGround;

    Vector3 playGroundScale;

    float playGroundRotation;
    float prevRotValue;

    int rotSign;  //To determine whether to rotate right or left


    /// <summary>
    /// Scales the playground according to the slider's value. Called on Slider's value changed event in the inspector
    /// </summary>
    public void Scale()
    {
        if(playGround == null)
        {
            playGround = ObjectSpawner.InstantiatedPlayGround.transform; //get instantiated AR playgorund object
        }
        playGroundScale = scaleSlider.value * Vector3.one * 2; //get the slider's value, multiplied by 2 for resoloution (can be changed as required)
        playGround.localScale = playGroundScale; //assing the chosen scale to the AR playgorund object
    }


    /// <summary>
    /// Rotates the playground according to the slider's value. Called on Slider's value changed event in the inspector
    /// </summary>
    public void Rotate()
    {
        if (playGround == null)
        {
            playGround = ObjectSpawner.InstantiatedPlayGround.transform; //get instantiated AR playgorund object
        }
        playGroundRotation = rotationSlider.value * 360 * Mathf.Deg2Rad;//get the rotation value in degrees
        rotSign = (int)((playGroundRotation - prevRotValue) / (Mathf.Abs(playGroundRotation - prevRotValue)));//get the direction of rotation, check if the slider is moving left or rigth
        playGround.Rotate(playGround.up, playGroundRotation * rotSign);//rotate the playground by the calculated angle around Y axis (horizontal rotation)
        prevRotValue = playGroundRotation;//update last rotation to be used in the next iteration for determining the rotation direction
    }
}
