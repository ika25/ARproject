using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO.Events;
public class GoalScored : MonoBehaviour
{
    [SerializeField] UserNames_SoList UserNames_SoList;
    [SerializeField] EventSO OnGoalScored;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            UserNames_SoList.userScores[UsersControl.currentUser] += 10;
            UsersControl.currentScore = UserNames_SoList.userScores[UsersControl.currentUser];
            Destroy(other.gameObject, 0.5f);
            OnGoalScored.Raise();
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Ball"))
    //    {
    //        UserNames_SoList.userScores[UsersControl.currentUser] += 10;
    //        UsersControl.currentScore =  UserNames_SoList.userScores[UsersControl.currentUser];
    //        OnGoalScored.Raise();
    //    }
    //}
}
