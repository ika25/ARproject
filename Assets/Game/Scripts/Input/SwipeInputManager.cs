using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInputManager : MonoBehaviour
{

    [SerializeField] ObjectSpawner objectSpawner;
    [SerializeField] Camera ARCam;
    Vector3 ThrowDirection;
    void Start()
    {
        ThrowDirection = Vector3.zero;
    }

    void Update()
    {

        if(Input.GetMouseButton(0))
        {
            RotateBall();
        }

    }

    private void RotateBall()
    {
        ThrowDirection = GetSecondPoint();

        objectSpawner.InstantiatedBall.transform.LookAt(ThrowDirection);
    }

    private Vector3 GetSecondPoint()
    {
        Ray camRay = ARCam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(camRay, out RaycastHit hit))
        {
            //if(hit.collider.CompareTag("Ground"))
            {
                return hit.point;
            }
        }
        return Vector3.zero;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(ThrowDirection, 0.05f);
    }
}
