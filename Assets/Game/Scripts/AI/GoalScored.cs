using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO.Events;

/// <summary>
/// Script for handling the goal scoring functionality
/// </summary>



public class GoalScored : MonoBehaviour
{
    [SerializeField] UserNames_SoList UserNames_SoList;
    [SerializeField] EventSO OnGoalScored;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            //UserNames_SoList.userScores[UsersControl.currentUser] += 10;  //increment score when ball hits the score collider
            //UsersControl.currentScore = UserNames_SoList.userScores[UsersControl.currentUser]; //save this user's score after the increment
            Destroy(other.gameObject, 0.5f);  //destroy ball
            OnGoalScored.Raise(); //fire OnGoalScored event to update the score text
        }
    }
}
