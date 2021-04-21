using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public void Shoot(ObjectSpawner objectSpawner )
    {
        Rigidbody ball_rb = objectSpawner.InstantiatedBall.GetComponent<Rigidbody>();
        ball_rb.AddForce(ball_rb.transform.forward * 5, ForceMode.Impulse);
    }
}
