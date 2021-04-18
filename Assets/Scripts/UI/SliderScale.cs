using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderScale : MonoBehaviour
{
    [SerializeField] ObjectSpawner ObjectSpawner;
    public void Scale()
    {
        Vector3 playGroundScale = this.GetComponent<Slider>().value * Vector3.one * 10;

        ObjectSpawner.InstantiatedPlayGround.transform.localScale = playGroundScale;
    }
}
