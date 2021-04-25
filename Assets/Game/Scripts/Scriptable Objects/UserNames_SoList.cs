using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a scriptable object used for the save system. 
/// The list contains user names, the dictionary contains scores as opposed to their respective users
/// The bool is used to clear all saved data on the first run of the game on a new device
/// </summary>


[CreateAssetMenu(fileName ="User names",menuName ="SO list")]
public class UserNames_SoList : ScriptableObject
{
    public List<string> UserNames;
    public Dictionary<string, int> userScores;
    public bool isFirstRun;
}
