using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="User names",menuName ="SO list")]
public class UserNames_SoList : ScriptableObject
{
    public List<string> UserNames;
    public Dictionary<string, int> userScores = new Dictionary<string, int>();
}
