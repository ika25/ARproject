using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script for handling the goalkeeper AI 
/// </summary>

public class GoalKeeper : MonoBehaviour
{
    [SerializeField] List<Transform> WayPoints;  //waypoints for the goalkeeper to move in between
    [Range(0.01f,0.05f)]
    [SerializeField] float speed = 0.1f;
    int currentWayPointIndex=0;

    void Update()
    {
        KeeperMove();
    }

    /// <summary>
    /// Moves goalkeeper backs and forth between the waypoints, at the speed defined in the inspector
    /// </summary>
    private void KeeperMove()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, WayPoints[currentWayPointIndex].position, speed);
        if(Vector3.Distance(this.transform.position,WayPoints[currentWayPointIndex].position) < 0.01f)
        {
            currentWayPointIndex++;
            currentWayPointIndex %= (WayPoints.Count); //to make sure the counter never exceeds the waypoints count
        }
    }
}
