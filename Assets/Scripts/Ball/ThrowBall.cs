using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public void Shoot(Rigidbody ball_rb)
    {
        ball_rb.AddForce(ball_rb.transform.forward * 4, ForceMode.Impulse);
    }
}
