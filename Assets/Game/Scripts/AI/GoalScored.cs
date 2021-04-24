using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScored : MonoBehaviour
{
    [SerializeField] UsersControl UsersControl;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            UsersControl.UserNamesList.userScores[UsersControl.currentUser] += 10;
            UsersControl.Score.text = UsersControl.UserNamesList.userScores[UsersControl.currentUser].ToString();
        }
    }
}
