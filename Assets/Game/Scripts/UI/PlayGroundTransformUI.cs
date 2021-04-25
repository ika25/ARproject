using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGroundTransformUI : MonoBehaviour
{
    [SerializeField] ObjectSpawner ObjectSpawner;

    [SerializeField] Slider scaleSlider;
    [SerializeField] Slider rotationSlider;

    //[SerializeField] Text scaletxt;
    //[SerializeField] Text rottxt;


    Transform playGround;

    Vector3 playGroundScale;

    float playGroundRotation;
    float prevRotValue;

    int rotSign;

    //private void Start()
    //{
    //    playGround = ObjectSpawner.InstantiatedPlayGround.transform;
    //}
    public void Scale()
    {
        if(playGround == null)
        {
            playGround = ObjectSpawner.InstantiatedPlayGround.transform; ;
        }
        playGroundScale = scaleSlider.value * Vector3.one * 2;
        //scaletxt.text = playGroundScale.ToString();
        playGround.localScale = playGroundScale;
    }

    public void Rotate()
    {
        if (playGround == null)
        {
            playGround = ObjectSpawner.InstantiatedPlayGround.transform; ;
        }
        playGroundRotation = rotationSlider.value * 360 * Mathf.Deg2Rad;
        rotSign = (int)((playGroundRotation - prevRotValue) / (Mathf.Abs(playGroundRotation - prevRotValue)));
        //rottxt.text = (rotSign * playGroundRotation).ToString();
        playGround.Rotate(playGround.up, playGroundRotation * rotSign);
        prevRotValue = playGroundRotation;
    }
}
