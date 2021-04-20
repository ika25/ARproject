using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInputManager : MonoBehaviour
{

    [SerializeField] Transform ball;
    Vector3 ThrowDirection;


    void Start()
    {
        ThrowDirection = Vector3.zero;
    }

    void Update()
    {

        if(Input.GetMouseButton(0))
        {
            ThrowDirection = GetSecondPoint();

            ball.LookAt(ThrowDirection);
        }

    }
    private Vector3 GetSecondPoint()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(camRay, out RaycastHit hit))
        {
            if(hit.collider.CompareTag("Ground"))
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
