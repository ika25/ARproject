using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsersControl : MonoBehaviour
{
    [SerializeField] GameObject PlayButton;
    [SerializeField] GameObject UsersDropDown;

    [SerializeField] Toggle newUserToggle;
    [SerializeField] Toggle existingUserToggle;

    [SerializeField] GameObject NewUserInput;


    public void ToggleSet(Toggle toggle)
    {
        if(toggle.isOn)
        {
            if(toggle == newUserToggle)
            {
                NewUserInput.SetActive(true);
                UsersDropDown.SetActive(false);
            }
            if(toggle == existingUserToggle)
            {
                UsersDropDown.SetActive(true);
                PlayButton.SetActive(true);
                NewUserInput.SetActive(false);
            }
        }
    }
}

