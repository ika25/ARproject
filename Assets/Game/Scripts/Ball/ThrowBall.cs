using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An experimental script for throwint the ball, just to test in Unity instead of building an APK everytime
/// </summary>


public class ThrowBall : MonoBehaviour
{
    [SerializeField] Rigidbody ball_rb;
    public void Shoot()
    {
        ball_rb.AddForce(ball_rb.transform.forward * 5, ForceMode.Impulse);
    }
}
