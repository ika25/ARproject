using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInputManager : MonoBehaviour
{
    Vector3 firstTouch;
    Vector3 secondTouch;


    Vector3 ScreenAxis;


    Vector3 screenBottomCenter;
    Vector3 ScreenTopCenter;
    void Start()
    {
        firstTouch = Vector3.zero;
        secondTouch = Vector3.zero;
        screenBottomCenter = Camera.main.ScreenToViewportPoint(new Vector3(Screen.width / 2, 0, 0));
        ScreenTopCenter = Camera.main.ScreenToViewportPoint(new Vector3(Screen.width / 2, Screen.height, 0));

        ScreenAxis = ScreenTopCenter - screenBottomCenter;

        Debug.DrawLine(screenBottomCenter,ScreenTopCenter);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            firstTouch = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        if(Input.GetMouseButtonUp(0))
        {
            secondTouch = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            //Debug.Log(firstTouch);
            //Debug.Log(secondTouch);

        }

        Debug.DrawLine(firstTouch, secondTouch);
    }

    private Vector3 GetThrowDirection()
    {
        Vector3 dir = (secondTouch - firstTouch).normalized;

        return dir;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(screenBottomCenter,0.1f);
        Gizmos.DrawSphere(ScreenTopCenter, 0.1f);



        Gizmos.color = Color.green;
        Gizmos.DrawSphere(firstTouch, 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(secondTouch, 0.1f);

    }
}
