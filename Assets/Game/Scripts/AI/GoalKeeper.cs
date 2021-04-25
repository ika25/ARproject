using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeper : MonoBehaviour
{
    [SerializeField] List<Transform> WayPoints;
    [Range(0.01f,0.05f)]
    [SerializeField] float speed = 0.1f;
    int currentWayPointIndex=0;

    void Update()
    {
        KeeperMove();
    }

    private void KeeperMove()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, WayPoints[currentWayPointIndex].position, speed);
        if(Vector3.Distance(this.transform.position,WayPoints[currentWayPointIndex].position) < 0.01f)
        {
            currentWayPointIndex++;
            currentWayPointIndex %= (WayPoints.Count);
        }
    }
}
